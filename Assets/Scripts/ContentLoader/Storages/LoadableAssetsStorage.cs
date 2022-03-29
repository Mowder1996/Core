using Common.Entities;
using ContentLoader.Interfaces;

namespace ContentLoader.Storages
{
    public class LoadableAssetsStorage : BaseKeyValueListStorage<string, ILoadable>
    {
        
    }
}