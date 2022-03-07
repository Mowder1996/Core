
namespace Common.Interfaces
{
    public interface IStorage<TItem>
    {
        int Count { get; }
        void Add(TItem item);
        TItem Get(int index);
        void Clear();
    }
}