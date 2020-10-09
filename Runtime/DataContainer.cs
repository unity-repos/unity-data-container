using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataContainer.Runtime
{
    public class DataContainer<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>
        where TK : IEquatable<TK>

    {
        private readonly Dictionary<TK, TV> _items;
        private readonly IIdentity<TK> _identity;

        public DataContainer(IIdentity<TK> identity)
        {
            _identity = identity;
            _items = new Dictionary<TK, TV>();
        }

        public bool Add(TK id, TV item)
        {
            if (_items.ContainsKey(id))
            {
                return false;
            }

            _items[id] = item;
            return true;
        }

        public TK Add(TV item)
        {
            while (true)
            {
                if (_items.ContainsKey(_identity.Value))
                {
                    if (_identity.IsMax())
                    {
                        throw new OverflowException();
                    }

                    _identity.Increase();
                }
                else
                {
                    break;
                }
            }

            _items[_identity.Value] = item;

            return _identity.Increase();
        }

        public void Update(TK id, TV item)
        {
            _items[id] = item;
        }

        public int Count()
        {
            return _items.Count;
        }


        public bool TryGet(TK id, out TV item)
        {
            item = default;
            if (!_items.ContainsKey(id))
            {
                return false;
            }

            item = _items[id];
            return true;
        }

        public bool Remove(TK id)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
                return true;
            }

            return false;
        }

        public bool Contains(TK id)
        {
            return _items.ContainsKey(id);
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}