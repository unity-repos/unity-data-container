using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace DataContainer.Runtime
{
    public class DataContainer<T> : IEnumerable<KeyValuePair<ulong, T>>
    {
        private readonly Dictionary<ulong, T> _items;
        private ulong identity;

        public DataContainer()
        {
            identity = 1;
            _items = new Dictionary<ulong, T>();
        }

        public bool Add(ulong id, T item)
        {
            if (_items.ContainsKey(id))
            {
                return false;
            }

            _items[id] = item;
            return true;
        }

        public ulong Add(T item)
        {
            while (true)
            {
                if (_items.ContainsKey(identity))
                {
                    if (identity >= ulong.MaxValue - 1)
                    {
                        throw new OverflowException();
                    }

                    identity++;
                }
                else
                {
                    break;
                }
            }


            _items[identity] = item;
            var temp = identity;
            identity++;

            return temp;
        }

        public void Update(ulong id, T item)
        {
            _items[id] = item;
        }

        public int Count()
        {
            return _items.Count;
        }


        public bool TryGet(ulong id, out T item)
        {
            item = default;
            if (!_items.ContainsKey(id))
            {
                return false;
            }

            item = _items[id];
            return true;
        }

        public bool Remove(ulong id)
        {
            if (_items.ContainsKey(id))
            {
                _items.Remove(id);
                return true;
            }

            return false;
        }

        public bool Contains(ulong id)
        {
            return _items.ContainsKey(id);
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}