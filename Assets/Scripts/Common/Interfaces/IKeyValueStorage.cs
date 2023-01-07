namespace Common.Interfaces
{
    public interface IKeyValueStorage<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        TValue Get(TKey key);
        void Remove(TKey key);
        void Clear();
    }
}