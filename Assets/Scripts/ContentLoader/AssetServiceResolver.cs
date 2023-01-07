using Zenject;
using Object = UnityEngine.Object;

namespace ContentLoader
{
    public class AssetServiceResolver
    {
        private readonly DiContainer _diContainer;
        
        public AssetServiceResolver(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public AssetService<T> Resolve<T>() where T : Object
        {
            return _diContainer.TryResolve<AssetService<T>>();
        }
    }
}