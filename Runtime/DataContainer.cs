using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DataContainers.Runtime
{
    public class DataContainer<T> : IEnumerable<KeyValuePair<ulong, T>>
        where T : IId
    {
        public delegate void DataHandler(T data);

        public DataHandler DelDataAdded { get; set; }
        public DataHandler DelDataRemoved { get; set; }
        public DataHandler DelDataUpdated { get; set; }


        private readonly Dictionary<ulong, T> _items;
        private ulong _identity;
        private readonly bool _conserveIds;

        private const ulong Start = 1;
        private static ulong Unassigned => Start - 1;

        public DataContainer(bool conserveIds = false)
        {
            _conserveIds = conserveIds;
            _identity = Start;
            _items = new Dictionary<ulong, T>();
        }


        public ulong Add(T item)
        {
            ulong id = Unassigned;

            if (_conserveIds)
            {
                if (item is IId i)
                {
                    if (id == Unassigned || i.Id >= Start && _items.ContainsKey(i.Id))
                    {
                        id = GetUniqueId();
                        i.Id = id;
                        if (item is Object obj)
                        {
#if UNITY_EDITOR
                            EditorUtility.SetDirty(obj);
#endif
                        }
                    }
                }
            }
            else
            {
                for (ulong i = _identity; i < ulong.MaxValue; i++)
                {
                    _identity = i;
                    if (_items.ContainsKey(_identity))
                    {
                        continue;
                    }

                    id = i;
                    break;
                }
            }

            Add(id, item);

            return id;
        }

        public bool Add(ulong id, T t)
        {
            if (t == null)
            {
                return false;
            }

            if (_items.ContainsKey(id))
            {
                Debug.LogError("Tried to add duplicate id!");
                return false;
            }

            _items[id] = t;
            t.Id = id;

            DelDataAdded?.Invoke(t);
            return true;
        }

        public void Update(ulong id, T item)
        {
            _items[id] = item;
            DelDataUpdated?.Invoke(item);
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
                var item = _items[id];
                _items.Remove(id);

                DelDataRemoved?.Invoke(item);
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

        private ulong GetUniqueId()
        {
            for (ulong i = Start; i < ulong.MaxValue; i++)
            {
                if (!_items.ContainsKey(i))
                {
                    return i;
                }
            }

            return Unassigned;
        }
    }
}