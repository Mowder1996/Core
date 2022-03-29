using Common.Interfaces;
using ContentLoader.Factories;
using ContentLoader.Storages;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Services
{
    public class ResourceLoaderService : IService
    {
        private readonly ResourceLoadTaskFactory _resourceLoadTaskFactory;
        private readonly ResourceHandlerFactory _resourceHandlerFactory;
        private readonly LoadTaskStorage _loadTaskStorage;
        private readonly LoadableAssetsStorage _assetsStorage;

        public ResourceLoaderService(ResourceLoadTaskFactory resourceLoadTaskFactory, 
                                    ResourceHandlerFactory resourceHandlerFactory, 
                                    LoadTaskStorage loadTaskStorage, 
                                    LoadableAssetsStorage assetsStorage)
        {
            _resourceLoadTaskFactory = resourceLoadTaskFactory;
            _loadTaskStorage = loadTaskStorage;
            _assetsStorage = assetsStorage;
            _resourceHandlerFactory = resourceHandlerFactory;
        }

        public async UniTask<TResourceType> LoadResource<TResourceType>(string key) where TResourceType : Object
        {
            var loadTask = _resourceLoadTaskFactory.Create(key);
            var handler = _resourceHandlerFactory.Create(loadTask);
            
            _loadTaskStorage.Add(key, loadTask);
            
            await handler.Load();

            if (handler.IsLoaded)
            {
                _assetsStorage.Add(key, handler);
            }
            
            return handler.Instance as TResourceType;
        }

        public void UnloadResources(string key)
        {
            var resourceHandlersCount = _assetsStorage.Count(key);

            for (var i = 0; i < resourceHandlersCount; i++)
            {
                _assetsStorage.Get(key, i).Unload();
            }
            
            _assetsStorage.Clear(key);
        }
    }
}