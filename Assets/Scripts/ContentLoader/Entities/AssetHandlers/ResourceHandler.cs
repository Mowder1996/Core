using ContentLoader.Entities.LoadTasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.AssetHandlers
{
    public class ResourceHandler : BaseAssetHandler<Object, ResourceLoadTask>
    {
        public ResourceHandler(ResourceLoadTask loadTask) : base(loadTask)
        {
        }
        
        public override async UniTask Load()
        {
            await base.Load();

            if (!IsLoaded)
            {
                return;
            }
            
            var instance = LoadTask.Result;

            Instance = instance;
        }

        public override void Unload()
        {
            Addressables.Release(Instance);
        }
    }
}