using Common.Interfaces;
using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Services
{
    public class PrefabSpawnerService : IService
    {
        private readonly PrefabLoadTaskFactory _prefabLoadTaskFactory;
        private readonly PrefabHandlerFactory _prefabHandlerFactory;
        
        public PrefabSpawnerService(PrefabLoadTaskFactory prefabLoadTaskFactory, 
                                    PrefabHandlerFactory prefabHandlerFactory)
        {
            _prefabLoadTaskFactory = prefabLoadTaskFactory;
            _prefabHandlerFactory = prefabHandlerFactory;
        }

        public async UniTask<PrefabHandler> SpawnPrefabFromFactory(string key)
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            await handler.Load();
            
            return handler;
        }
        
        public async UniTask<PrefabHandler<TComponent>> SpawnPrefabFromFactory<TComponent>(string key) where TComponent : Component
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create<TComponent>(loadTask);

            await handler.Load();
            
            return handler;
        }
    }
}