using System.Collections.Generic;
using Common.Entities;
using ContentLoader.Interfaces;

namespace ContentLoader.Storages
{
    public class LoadTaskStorage : BaseKeyValueStorage<string, ILoadTask>
    {
        public LoadTaskStorage(Dictionary<string, ILoadTask> items) : base(items)
        {
        }

        public override void Add(string key, ILoadTask value)
        {
            Remove(key);
            
            base.Add(key, value);
        }
    }
}