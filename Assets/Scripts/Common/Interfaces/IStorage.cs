
namespace Common.Interfaces
{
    public interface IStorage<TItem>
    {
        //TODO: add Remove()
        //TODO: add base entity
        int Count { get; }
        void Add(TItem item);
        TItem Get(int index);
        void Clear();
    }
}