using System.Collections.Generic;
using Common.Interfaces;
using ContentLoader.Data;
using ContentLoader.Factories;
using ContentLoader.Storages;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Services
{
    public class CatalogLoaderService : IService
    {
        private readonly CatalogLoadTaskFactory _catalogLoadTaskFactory;
        private readonly CatalogUpdateTaskFactory _catalogUpdateTaskFactory;
        private readonly LoadTaskStorage _loadTaskStorage;

        public CatalogLoaderService(CatalogLoadTaskFactory catalogLoadTaskFactory, 
                                    CatalogUpdateTaskFactory catalogUpdateTaskFactory, 
                                    LoadTaskStorage loadTaskStorage)
        {
            _catalogLoadTaskFactory = catalogLoadTaskFactory;
            _catalogUpdateTaskFactory = catalogUpdateTaskFactory;
            _loadTaskStorage = loadTaskStorage;
        }

        public async UniTask<bool> LoadCatalog(string catalogPath)
        {
            var catalogLoad = _catalogLoadTaskFactory.Create(catalogPath);

            _loadTaskStorage.Add(catalogPath, catalogLoad);
            
            await catalogLoad.Execute();

            return catalogLoad.Status.Equals(LoadStatus.Success);
        }

        public async UniTask<IEnumerable<string>> GetOutDatedCatalogs()
        {
            return await Addressables.CheckForCatalogUpdates();
        }

        public async UniTask<bool> UpdateCatalog(string catalogPath)
        {
            return await UpdateCatalogsInternal(new[] {catalogPath});
        }

        public async UniTask<bool> UpdateCatalogs(IEnumerable<string> catalogPaths)
        {
            return await UpdateCatalogsInternal(catalogPaths);
        }

        private async UniTask<bool> UpdateCatalogsInternal(IEnumerable<string> catalogPaths)
        {
            foreach (var catalogPath in catalogPaths)
            {
                var task = _catalogUpdateTaskFactory.Create(catalogPath);

                _loadTaskStorage.Add(catalogPath, task);
                
                await task.Execute();

                if (!task.Status.Equals(LoadStatus.Success))
                {
                    return false;
                }
            }

            return true;
        }
    }
}