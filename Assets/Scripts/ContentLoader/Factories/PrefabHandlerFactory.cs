using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Entities.LoadTasks;
using UnityEngine;
using Zenject;

namespace ContentLoader.Factories
{
    public class PrefabHandlerFactory : PlaceholderFactory<PrefabLoadTask, PrefabHandler>
    {
        private readonly PrefabInjectionFactory _prefabInjectionFactory;

        public PrefabHandlerFactory(PrefabInjectionFactory prefabInjectionFactory)
        {
            _prefabInjectionFactory = prefabInjectionFactory;
        }

        public override PrefabHandler Create(PrefabLoadTask loadTask)
        {
            return new PrefabHandler(loadTask, _prefabInjectionFactory);
        }

        public PrefabHandler<TComponent> Create<TComponent>(PrefabLoadTask loadTask) where TComponent : Component
        {
            return new PrefabHandler<TComponent>(loadTask, _prefabInjectionFactory);
        }
    }
}