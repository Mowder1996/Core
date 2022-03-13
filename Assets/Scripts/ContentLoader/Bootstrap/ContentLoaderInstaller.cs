using Common.Extensions.Zenject;
using ContentLoader.Entities;
using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using ContentLoader.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
            Container.BindFactory<string, CatalogLoadTask, CatalogLoadTaskFactory>().AsSingle();
            Container.BindFactory<string, PrefabLoadTask, PrefabLoadTaskFactory>().AsSingle();
            Container.BindFactory<PrefabLoadTask, PrefabHandler, PrefabHandlerFactory>().AsSingle();
            Container.BindFactory<GameObject, GameObject, PrefabInjectionFactory>().AsSingle();
        }

        private void InstallServices()
        {
            Container.InstallService<CatalogLoaderService>();
            Container.InstallService<PrefabSpawnerService>();
        }
    }
}