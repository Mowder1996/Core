using Common.Interfaces;
using Zenject;

namespace Common.Extensions.Zenject
{
    public static class InstallingExtensions
    {
        public static void InstallFacade<TFacade>(this DiContainer container) where TFacade : IFacade
        {
            container.Bind<TFacade>().AsSingle();
        }

        public static void InstallModel<TModel>(this DiContainer container) where TModel : IModel
        {
            container.BindInterfacesAndSelfTo<TModel>().AsSingle();
        }
        
        public static void InstallStorage<TProduct, TStorage>(this DiContainer container) 
            where TStorage : IStorage<TProduct>
        {
            container.BindInterfacesAndSelfTo<TStorage>().AsSingle();
        }

        public static void InstallStorage<TKey, TValue, TStorage>(this DiContainer container) 
            where TStorage : IKeyValueStorage<TKey, TValue>
        {
            container.BindInterfacesAndSelfTo<TStorage>().AsSingle();
        }

        public static void InstallService<TService>(this DiContainer container) where TService : IService
        {
            container.BindInterfacesAndSelfTo<TService>().AsSingle();
        }
    }
}