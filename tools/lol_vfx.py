"""Parse ritobin text (from `ritobin-tools convert`) for VfxSystemDefinitionData entries
and resolve field names through tools/hashes/hashes.binfields.txt.

Node forms:
  {'t': 'block', 'cls': str, 'fields': [(name, node), ...]}   # embed / pointer / entry with { ... }
  {'t': 'list', 'items': [node, ...]}                         # list[...] = { ... }
  {'t': 'scalar', 'ty': str, 'v': value}                      # f32 / u8 / string / vecN / ...
"""
import os
import re

HASH_DIR = os.path.join(os.path.dirname(os.path.abspath(__file__)), 'hashes')


class _Multi(list):
    pass


def load_names(path=os.path.join(HASH_DIR, 'hashes.binfields.txt')):
    names = {}
    with open(path, errors='ignore') as f:
        for line in f:
            p = line.rstrip('\n').split(' ', 1)
            if len(p) == 2:
                names[int(p[0], 16)] = p[1]
    return names


_FIELD = re.compile(r'^(0x[0-9a-fA-F]+|[A-Za-z_][\w]*):\s*([\w\[\]]+)\s*=\s*(.*)$')
_ITEM_BLOCK = re.compile(r'^(0x[0-9a-fA-F]+)\s*\{\s*$')
_ITEM_CLOSED = re.compile(r'^(0x[0-9a-fA-F]+)\s*\{\s*\}\s*$')


def _parse_scalar(ty, raw):
    raw = raw.strip()
    if raw.startswith('{'):
        inner = raw.strip('{} ')
        items = []
        for x in inner.split(','):
            x = x.strip()
            if not x:
                continue
            try:
                items.append(float(x))
            except ValueError:
                items.append(x)
        return {'t': 'scalar', 'ty': ty, 'v': items}
    if raw.startswith('"'):
        return {'t': 'scalar', 'ty': ty, 'v': raw.strip('"')}
    if raw in ('true', 'false'):
        return {'t': 'scalar', 'ty': ty, 'v': raw == 'true'}
    if raw.startswith('0x'):
        return {'t': 'scalar', 'ty': ty, 'v': int(raw, 16)}
    try:
        return {'t': 'scalar', 'ty': ty, 'v': float(raw)}
    except ValueError:
        return {'t': 'scalar', 'ty': ty, 'v': raw}


def parse_text(lines):
    """Return the list of top-level entries as (key_hex, node)."""
    root = {'t': 'block', 'cls': None, 'fields': []}
    stack = [root]
    out = []
    for raw in lines:
        line = raw.strip()
        if not line or line.startswith('#'):
            continue
        if line == '}':
            stack.pop()
            continue
        parent = stack[-1]
        # Top-level entries: "0xKEY = 0xTYPE {"
        m = re.match(r'^(0x[0-9a-fA-F]+)\s*=\s*(0x[0-9a-fA-F]+)\s*\{\s*$', line)
        if m and parent is root:
            node = {'t': 'block', 'cls': m.group(2), 'fields': []}
            out.append((m.group(1), node))
            stack.append(node)
            continue
        # Field lines: "key: type = value"
        m = _FIELD.match(line)
        if m and isinstance(parent, dict) and parent.get('t') in ('block',):
            key, ty, rest = m.group(1), m.group(2), m.group(3)
            if rest.endswith('{') or rest.endswith('{ }') or rest == '{':
                cls = None
                mm = re.match(r'^(0x[0-9a-fA-F]+)\s*\{', rest)
                if mm:
                    cls = mm.group(1)
                if ty.startswith('list'):
                    node = {'t': 'list', 'items': []}
                else:
                    node = {'t': 'block', 'cls': cls, 'fields': []}
                if rest.endswith('{ }') or rest.endswith('{}'):
                    pass  # closed empty block: do not push
                else:
                    parent['fields'].append((key, node))
                    stack.append(node)
                    continue
                parent['fields'].append((key, node))
                continue
            if rest.startswith('{') and ty.startswith('list') is False and rest.endswith('}'):
                parent['fields'].append((key, _parse_scalar(ty, rest)))
                continue
            if rest.startswith('{') and ty.startswith('list') and not rest.endswith('}'):
                node = {'t': 'list', 'items': []}
                parent['fields'].append((key, node))
                stack.append(node)
                continue
            parent['fields'].append((key, _parse_scalar(ty, rest)))
            continue
        # List item block: "0xCLASS {" or closed "0xCLASS { }"
        if parent.get('t') == 'list':
            m = _ITEM_BLOCK.match(line)
            if m:
                node = {'t': 'block', 'cls': m.group(1), 'fields': []}
                parent['items'].append(node)
                stack.append(node)
                continue
            m = _ITEM_CLOSED.match(line)
            if m:
                parent['items'].append({'t': 'block', 'cls': m.group(1), 'fields': []})
                continue
            if line.startswith('{'):
                parent['items'].append(_parse_scalar('item', line))
                continue
            parent['items'].append(_parse_scalar('item', line))
            continue
        # Unknown line: keep as raw scalar in the current block
        parent.setdefault('raw', []).append(line)
    return out


def resolve(node, names):
    """Return a plain python view where keys are resolved names (hex if unknown)."""
    def key(k):
        if k.startswith('0x'):
            v = int(k, 16)
            return names.get(v, k)
        return k
    if node['t'] == 'block':
        d = {'_cls': node.get('cls')}
        for k, child in node['fields']:
            name = key(k)
            val = resolve(child, names)
            if name in d and name != '_cls':
                # repeated field name: collect into a list
                if not isinstance(d[name], _Multi):
                    d[name] = _Multi([d[name]])
                d[name].append(val)
            else:
                d[name] = val
        return d
    if node['t'] == 'list':
        return [resolve(i, names) for i in node['items']]
    return node['v']
