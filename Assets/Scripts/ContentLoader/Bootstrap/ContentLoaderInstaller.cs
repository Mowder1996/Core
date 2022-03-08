using Common.Extensions.Zenject;
using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using ContentLoader.Services;
using Zenject;

namespace ContentLoader.Bootstrap
{
    public class ContentLoaderInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFacade<ContentLoader>();

            InstallFactories();
            InstallServices();
        }

        private void InstallFactories()
        {
            Container.BindFactory<CatalogLoadTask, CatalogLoadTaskFactory>().AsSingle();
        }

        private void InstallServices()
        {
            Container.InstallService<CatalogLoaderService>();
        }
    }
}