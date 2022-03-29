using Common.Extensions.Zenject;
using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using ContentLoader.Interfaces;
using ContentLoader.Services;
using ContentLoader.Storages;
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
            InstallStorages();
        }

        private void InstallFactories()
        {
            Container.BindFactory<string, CatalogLoadTask, CatalogLoadTaskFactory>().AsSingle();
            Container.BindFactory<string, CatalogUpdateTask, CatalogUpdateTaskFactory>().AsSingle();
            Container.BindFactory<string, PrefabLoadTask, PrefabLoadTaskFactory>().AsSingle();
            Container.BindFactory<string, ResourceLoadTask, ResourceLoadTaskFactory>().AsSingle();
            
            Container.BindFactory<PrefabLoadTask, PrefabHandler, PrefabHandlerFactory>().AsSingle();
            Container.BindFactory<GameObject, GameObject, PrefabInjectionFactory>().AsSingle();

            Container.BindFactory<ResourceLoadTask, ResourceHandler, ResourceHandlerFactory>().AsSingle();
        }

        private void InstallServices()
        {
            Container.InstallService<CatalogLoaderService>();
            Container.InstallService<PrefabSpawnerService>();
            Container.InstallService<ResourceLoaderService>();
        }
        
        private void InstallStorages()
        {
            Container.InstallStorage<string, ILoadTask, LoadTaskStorage>();
            Container.InstallStorage<string, ILoadable, LoadableAssetsStorage>();
        }
    }
}