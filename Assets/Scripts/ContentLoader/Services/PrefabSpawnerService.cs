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
        private readonly PrefabInjectionFactory _prefabInjectionFactory;
        private readonly LoadableAssetsStorage _assetsStorage;
        
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
            var handler = await LoadPrefabHandler(key);

            _assetsStorage.Add(key, handler);
            
            return _prefabInjectionFactory.Create(handler.Instance);
        }
        
        public async UniTask<TComponent> CreatePrefab<TComponent>(string key)
            where TComponent : Component
        {
            var handler = await LoadPrefabHandler(key);
            
            _assetsStorage.Add(key, handler);

            return _prefabInjectionFactory.CreateForComponent<TComponent>(handler.Instance);
        }

        public void UnloadPrefab(string key)
        {
            var prefabHandlersCount = _assetsStorage.Count(key);

            for (var i = 0; i < prefabHandlersCount; i++)
            {
                _assetsStorage.Get(key, i).Unload();
            }

            _assetsStorage.Clear(key);
        }

        private async UniTask<PrefabHandler> LoadPrefabHandler(string key)
        {
            var loadTask = _prefabLoadTaskFactory.Create(key);
            var handler = _prefabHandlerFactory.Create(loadTask);

            await handler.Load();
            
            return handler;
        }
    }
}