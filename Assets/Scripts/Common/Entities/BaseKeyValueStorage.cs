using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Entities
{
    public class BaseKeyValueStorage<TKey, TValue> : IKeyValueStorage<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _items;

        public BaseKeyValueStorage(Dictionary<TKey, TValue> items)
        {
            _items = items;
        }

        public virtual void Add(TKey key, TValue value)
        {
            if (_items.ContainsKey(key))
            {
                return;
            }
            
            _items.Add(key, value);
        }

        public virtual TValue Get(TKey key)
        {
            return _items.ContainsKey(key) ? _items[key] : default;
        }

        public void Remove(TKey key)
        {
            if (!_items.ContainsKey(key))
            {
                return;
            }

            _items.Remove(key);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}