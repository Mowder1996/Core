using Common.Interfaces;
using ContentLoader.Data;
using ContentLoader.Factories;
using Cysharp.Threading.Tasks;

namespace ContentLoader.Services
{
    public class CatalogLoaderService : IService
    {
        private readonly CatalogLoadTaskFactory _catalogLoadTaskFactory;

        public CatalogLoaderService(CatalogLoadTaskFactory catalogLoadTaskFactory)
        {
            _catalogLoadTaskFactory = catalogLoadTaskFactory;
        }

        public async UniTask<bool> LoadCatalog(string catalogPath)
        {
            var catalogLoad = _catalogLoadTaskFactory.Create();

            await catalogLoad.Execute(catalogPath);

            return catalogLoad.Status.Equals(LoadStatus.Success);
        }
    }
}