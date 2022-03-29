using System;
using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using ContentLoader.Data;
using ContentLoader.Services;
using ContentLoader.Storages;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ContentLoader
{
    public class ContentLoader : IFacade
    {
        private readonly CatalogLoaderService _catalogLoaderService;
        private readonly PrefabSpawnerService _prefabSpawnerService;
        private readonly ResourceLoaderService _resourceLoaderService;
        private readonly LoadTaskStorage _loadTaskStorage;

        public ContentLoader(CatalogLoaderService catalogLoaderService, 
                            PrefabSpawnerService prefabSpawnerService, 
                            ResourceLoaderService resourceLoaderService, 
                            LoadTaskStorage loadTaskStorage)
        {
            _catalogLoaderService = catalogLoaderService;
            _prefabSpawnerService = prefabSpawnerService;
            _resourceLoaderService = resourceLoaderService;
            _loadTaskStorage = loadTaskStorage;
        }

        public IEnumerable<IObservable<float>> GetLoadStreamsByKey(string key)
        {
            var loadStreamsCount = _loadTaskStorage.Count(key);
            
            if (loadStreamsCount == 0)
            {
                return null;
            }

            var progressStreams = new List<IObservable<float>>();

            for (var i = 0; i < loadStreamsCount; i++)
            {
                var loadTask = _loadTaskStorage.Get(key, i);
                
                if (!loadTask.Status.Equals(LoadStatus.Process))
                {
                    continue;
                }
                
                progressStreams.Add(loadTask.ProgressStream);
            }

            return progressStreams.Any() ? progressStreams : null;
        }

        #region Catalogs

        public UniTask<bool> LoadCatalog(string catalogPath)
        {
            return _catalogLoaderService.LoadCatalog(catalogPath);
        }

        public UniTask<bool> UpdateCatalog(string catalogPath)
        {
            return _catalogLoaderService.UpdateCatalog(catalogPath);
        }

        public async UniTask<bool> UpdateCatalogs()
        {
            var outDatedCatalogs = await _catalogLoaderService.GetOutDatedCatalogs();

            return await _catalogLoaderService.UpdateCatalogs(outDatedCatalogs);
        }

        #endregion

        #region Prefabs

        public UniTask<GameObject> CreatePrefab(string key)
        {
            return _prefabSpawnerService.CreatePrefab(key);
        }

        public UniTask<TComponent> CreatePrefab<TComponent>(string key) where TComponent : Component
        {
            return _prefabSpawnerService.CreatePrefab<TComponent>(key);
        }

        public void DestroyAllPrefabsByKey(string key)
        {
            _prefabSpawnerService.DestroyPrefabs(key);
        }

        #endregion

        #region Resources

        public UniTask<TResourceType> LoadResource<TResourceType>(string key) where TResourceType : Object
        {
            return _resourceLoaderService.LoadResource<TResourceType>(key);
        }

        public void UnloadAllResourcesByKey(string key)
        {
            _resourceLoaderService.UnloadResources(key);
        }

        #endregion
    }
}
