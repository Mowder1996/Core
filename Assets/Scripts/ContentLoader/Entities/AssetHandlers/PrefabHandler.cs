using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.AssetHandlers
{
    public class PrefabHandler : BasePrefabHandler<GameObject, PrefabLoadTask>
    {
        public PrefabHandler(PrefabLoadTask loadTask, PrefabInjectionFactory prefabInjectionFactory) 
            : base(loadTask, prefabInjectionFactory)
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

            PrefabInjectionFactory.Create(instance);

            Instance = instance;
        }

        public override void Unload()
        {
            Addressables.ReleaseInstance(Instance);
        }
    }

    public class PrefabHandler<TComponent> : BasePrefabHandler<TComponent, PrefabLoadTask> where TComponent : Component
    {
        public PrefabHandler(PrefabLoadTask loadTask, PrefabInjectionFactory prefabInjectionFactory) 
            : base(loadTask, prefabInjectionFactory)
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

            Instance = PrefabInjectionFactory.CreateForComponent<TComponent>(instance.gameObject);
        }
        
        public override void Unload()
        {
            Addressables.ReleaseInstance(Instance.gameObject);
        }
    }
}