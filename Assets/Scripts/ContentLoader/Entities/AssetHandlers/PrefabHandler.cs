using ContentLoader.Entities.LoadTasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.AssetHandlers
{
    public class PrefabHandler : BaseAssetHandler<GameObject, PrefabLoadTask>
    {
        public PrefabHandler(PrefabLoadTask loadTask) 
            : base(loadTask)
        {
        }

        public override void Unload()
        {
            Addressables.ReleaseInstance(Instance);
        }
    }
}