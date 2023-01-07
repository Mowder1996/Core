using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Entities
{
    public class BaseKeyValueListStorage<TKey, TValue> : IKeyValueListStorage<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> _items = new Dictionary<TKey, List<TValue>>();


        public void Add(TKey key, TValue value)
        {
            if (!_items.ContainsKey(key))
            {
                _items.Add(key, new List<TValue>());
            }
            
            _items[key].Add(value);
        }

        public int Count(TKey key)
        {
            return GetCount(key);
        }

        public TValue Get(TKey key, int index)
        {
            if (!_items.ContainsKey(key))
            {
                return default;
            }

            var count = GetCount(key);

            if (index >= count)
            {
                return default;
            }

            return _items[key][index];
        }

        public void Remove(TKey key, TValue value)
        {
            if (!_items.ContainsKey(key))
            {
                return;
            }

            if (_items[key].Contains(value))
            {
                return;
            }
            
            _items[key].Remove(value);
        }

        public void Clear(TKey key)
        {
            if (!_items.ContainsKey(key))
            {
                return;
            }
            
            _items[key].Clear();
        }

        public void Clear()
        {
            _items.Clear();
        }

        private int GetCount(TKey key)
        {
            return _items.ContainsKey(key) ? _items[key].Count : 0;
        }
    }
}