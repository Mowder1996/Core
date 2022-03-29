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
        private readonly LoadableAssetsStorage _assetsStorage;
        
        public PrefabSpawnerService(PrefabLoadTaskFactory prefabLoadTaskFactory, 
                                    PrefabHandlerFactory prefabHandlerFactory, 
                                    LoadTaskStorage loadTaskStorage, 
                                    LoadableAssetsStorage assetsStorage)
        {
            _prefabLoadTaskFactory = prefabLoadTaskFactory;
            _prefabHandlerFactory = prefabHandlerFactory;
            _loadTaskStorage = loadTaskStorage;
            _assetsStorage = assetsStorage;
        }

        public async UniTask<GameObject> CreatePrefab(string key)
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            _loadTaskStorage.Add(key, loadTask);
            
            await handler.Load();

            if (handler.IsLoaded)
            {
                _assetsStorage.Add(key, handler);
            }
            
            return handler.Instance;
        }
        
        public async UniTask<TComponent> CreatePrefab<TComponent>(string key)
            where TComponent : Component
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create<TComponent>(loadTask);

            _loadTaskStorage.Add(key, loadTask);
            
            await handler.Load();

            if (handler.IsLoaded)
            {
                var componentHandler = handler as PrefabHandler<Component>;
                
                _assetsStorage.Add(key, componentHandler);
            }
            
            return handler.Instance;
        }

        public void DestroyPrefabs(string key)
        {
            var prefabHandlersCount = _assetsStorage.Count(key);

            for (var i = 0; i < prefabHandlersCount; i++)
            {
                _assetsStorage.Get(key, i).Unload();
            }
            
            _assetsStorage.Clear(key);
        }
    }
}