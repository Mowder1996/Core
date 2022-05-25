using System.Collections.Generic;
using Common.Interfaces;
using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Services
{
    public class CatalogLoaderService : IService
    {
        private readonly CatalogLoadTaskFactory _catalogLoadTaskFactory;
        private readonly CatalogUpdateTaskFactory _catalogUpdateTaskFactory;
        private readonly CatalogsUpdateTaskFactory _catalogsUpdateTaskFactory;

        public CatalogLoaderService(CatalogLoadTaskFactory catalogLoadTaskFactory, 
                                    CatalogUpdateTaskFactory catalogUpdateTaskFactory, 
                                    CatalogsUpdateTaskFactory catalogsUpdateTaskFactory)
        {
            _catalogLoadTaskFactory = catalogLoadTaskFactory;
            _catalogUpdateTaskFactory = catalogUpdateTaskFactory;
            _catalogsUpdateTaskFactory = catalogsUpdateTaskFactory;
        }

        public UniTask LoadCatalog(string catalogPath)
        {
            var catalogLoad = _catalogLoadTaskFactory.Create(catalogPath);

            return catalogLoad.Execute();
        }

        public async UniTask<IEnumerable<string>> GetOutDatedCatalogs()
        {
            return await Addressables.CheckForCatalogUpdates();
        }

        public UniTask UpdateCatalog(string catalogPath)
        {
            return UpdateCatalogsInternal(new[] {catalogPath});
        }

        public UniTask UpdateCatalogs(IEnumerable<string> catalogPaths)
        {
            return UpdateCatalogsInternal(catalogPaths);
        }

        private UniTask UpdateCatalogsInternal(IEnumerable<string> catalogPaths)
        {
            var updateTasks = new List<CatalogUpdateTask>();
            
            foreach (var catalogPath in catalogPaths)
            {
                var task = _catalogUpdateTaskFactory.Create(catalogPath);

                updateTasks.Add(task);
            }

            var catalogsUpdateTask = _catalogsUpdateTaskFactory.Create(updateTasks);

            return catalogsUpdateTask.Execute();
        }
    }
}