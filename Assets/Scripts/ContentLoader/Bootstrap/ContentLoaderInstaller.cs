using Common.Extensions.Zenject;
using ContentLoader.Services;
using Zenject;

namespace ContentLoader.Installers
{
    public class ContentLoaderInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFacade<ContentLoader>();

            InstallServices();
        }

        private void InstallServices()
        {
            Container.InstallService<CatalogLoaderService>();
        }
    }
}