using Common.Interfaces;
using ContentLoader.Services;
using Cysharp.Threading.Tasks;

namespace ContentLoader
{
    public class ContentLoader : IFacade
    {
        private CatalogLoaderService _catalogLoaderService;

        public ContentLoader(CatalogLoaderService catalogLoaderService)
        {
            _catalogLoaderService = catalogLoaderService;
        }

        public UniTask<bool> LoadCatalog(string catalogPath)
        {
            return _catalogLoaderService.LoadCatalog(catalogPath);
        }
    }
}
