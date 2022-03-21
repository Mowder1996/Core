using Common.Entities;
using ContentLoader.Interfaces;

namespace ContentLoader.Storages
{
    public class LoadTaskStorage : BaseKeyValueStorage<string, ILoadTask>
    {
        public override void Add(string key, ILoadTask value)
        {
            Remove(key);
            
            base.Add(key, value);
        }
    }
}