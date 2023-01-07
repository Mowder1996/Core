using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace ContentLoader
{
    public class ContentLoaderInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ContentLoader>().AsSingle();
            Container.Bind<AssetServiceResolver>().AsSingle();
            
            InstallNewAssetType<GameObject>();
            InstallNewAssetType<Sprite>();
            InstallNewAssetType<Texture2D>();
            InstallNewAssetType<Material>();
        }

        protected virtual void InstallNewAssetType<T>() where T : Object
        {
            Container.Bind<AssetStorage<T>>().AsSingle();
            Container.BindFactory<string, AssetHandler<T>, AssetFactory<T>>().AsSingle();
            Container.Bind<AssetService<T>>().AsSingle();
        }
    }
}