using ContentLoader.Factories;
using ContentLoader.Interfaces;
using UnityEngine;

namespace ContentLoader.Entities.AssetHandlers
{
    public abstract class BasePrefabHandler<T, TLoadTask> : BaseAssetHandler<T, TLoadTask>
        where T : Object
        where TLoadTask : ILoadTask
    {
        protected readonly PrefabInjectionFactory PrefabInjectionFactory;
        
        protected BasePrefabHandler(TLoadTask loadTask, PrefabInjectionFactory prefabInjectionFactory) : base(loadTask)
        {
            PrefabInjectionFactory = prefabInjectionFactory;
        }
    }
}