
namespace Common.Interfaces
{
    public interface IKeyValueListStorage<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        int Count(TKey key);
        TValue Get(TKey key, int index);
        void Remove(TKey key, TValue value);
        void Clear(TKey key);
        void Clear();
    }
}