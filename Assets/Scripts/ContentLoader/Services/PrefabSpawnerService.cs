using Common.Interfaces;
using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Factories;
using ContentLoader.Storages;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Services
{
    public class PrefabSpawnerService : IService
    {
        private readonly PrefabLoadTaskFactory _prefabLoadTaskFactory;
        private readonly PrefabHandlerFactory _prefabHandlerFactory;
        private readonly LoadTaskStorage _loadTaskStorage;
        
        public PrefabSpawnerService(PrefabLoadTaskFactory prefabLoadTaskFactory, 
                                    PrefabHandlerFactory prefabHandlerFactory, 
                                    LoadTaskStorage loadTaskStorage)
        {
            _prefabLoadTaskFactory = prefabLoadTaskFactory;
            _prefabHandlerFactory = prefabHandlerFactory;
            _loadTaskStorage = loadTaskStorage;
        }

        public async UniTask<PrefabHandler> SpawnPrefab(string key)
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            _loadTaskStorage.Add(key, loadTask);
            
            await handler.Load();
            
            return handler;
        }
        
        public async UniTask<PrefabHandler<TComponent>> SpawnPrefab<TComponent>(string key)
            where TComponent : Component
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create<TComponent>(loadTask);

            _loadTaskStorage.Add(key, loadTask);
            
            await handler.Load();
            
            return handler;
        }
    }
}