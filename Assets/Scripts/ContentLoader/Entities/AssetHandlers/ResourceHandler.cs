using ContentLoader.Entities.LoadTasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.AssetHandlers
{
    public class ResourceHandler : BaseAssetHandler<Object, ResourceLoadTask>
    {
        public ResourceHandler(ResourceLoadTask loadTask) : base(loadTask)
        {
        }

        public override void Unload()
        {
            Addressables.Release(Instance);
        }
    }
}