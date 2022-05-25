using Common.Interfaces;
using ContentLoader.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Services
{
    public class PrefabSpawnerService : IService
    {
        private readonly PrefabLoadTaskFactory _prefabLoadTaskFactory;
        private readonly PrefabHandlerFactory _prefabHandlerFactory;
        private readonly PrefabInjectionFactory _prefabInjectionFactory;
        
        public PrefabSpawnerService(PrefabLoadTaskFactory prefabLoadTaskFactory, 
                                    PrefabHandlerFactory prefabHandlerFactory, 
                                    PrefabInjectionFactory prefabInjectionFactory)
        {
            _prefabLoadTaskFactory = prefabLoadTaskFactory;
            _prefabHandlerFactory = prefabHandlerFactory;
            _prefabInjectionFactory = prefabInjectionFactory;
        }

        public async UniTask<GameObject> CreatePrefab(string key)
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            await handler.Load();

            return _prefabInjectionFactory.Create(handler.Instance);
        }
        
        public async UniTask<TComponent> CreatePrefab<TComponent>(string key)
            where TComponent : Component
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            await handler.Load();

            return _prefabInjectionFactory.CreateForComponent<TComponent>(handler.Instance);
        }
    }
}