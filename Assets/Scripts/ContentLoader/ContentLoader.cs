using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using ContentLoader.Services;
using ContentLoader.Storages;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace ContentLoader
{
    public class ContentLoader : IFacade
    {
        private readonly CatalogLoaderService _catalogLoaderService;
        private readonly PrefabSpawnerService _prefabSpawnerService;
        private readonly ResourceLoaderService _resourceLoaderService;
        private readonly LoadableAssetsStorage _assetsStorage;

        public ContentLoader(CatalogLoaderService catalogLoaderService, 
                            PrefabSpawnerService prefabSpawnerService, 
                            ResourceLoaderService resourceLoaderService, 
                            LoadableAssetsStorage assetsStorage)
        {
            _catalogLoaderService = catalogLoaderService;
            _prefabSpawnerService = prefabSpawnerService;
            _resourceLoaderService = resourceLoaderService;
            _assetsStorage = assetsStorage;
        }

        public async UniTask<IEnumerable<string>> GetKeysByLabels(IEnumerable<string> labels, Addressables.MergeMode mergeMode)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(labels, mergeMode);

            return locations.Select(item => item.PrimaryKey);
        }
        
        public async UniTask<IEnumerable<string>> GetKeysByLabels<T>(IEnumerable<string> labels, Addressables.MergeMode mergeMode)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(labels, mergeMode, typeof(T));

            return locations.Select(item => item.PrimaryKey);
        }

        #region Catalogs

        public UniTask LoadCatalog(string catalogPath)
        {
            return _catalogLoaderService.LoadCatalog(catalogPath);
        }

        public UniTask UpdateCatalog(string catalogPath)
        {
            return _catalogLoaderService.UpdateCatalog(catalogPath);
        }

        public UniTask UpdateCatalog(IEnumerable<string> catalogPaths)
        {
            return _catalogLoaderService.UpdateCatalogs(catalogPaths);
        }
        
        public async UniTask<IEnumerable<string>> GetOutDatedCatalogs()
        {
            return await _catalogLoaderService.GetOutDatedCatalogs();
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
            if (_assetsStorage.Count(key) > 0)
            {
                var handler = _assetsStorage.Get(key, 0);
            }
            
            return _resourceLoaderService.LoadResource<TResourceType>(key);
        }

        public void UnloadAllResourcesByKey(string key)
        {
            _resourceLoaderService.UnloadResources(key);
        }

        #endregion
    }
}
