using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader
{
    public class AssetService<T> where T : Object
    {
        private readonly AssetStorage<T> _assetStorage;
        private readonly AssetFactory<T> _assetFactory;

        public AssetService(AssetStorage<T> assetService, AssetFactory<T> assetFactory)
        {
            _assetStorage = assetService;
            _assetFactory = assetFactory;
        }

        public async UniTask<(string, T)> CreateAsset(string key)
        {
            var assetHandler = _assetFactory.Create(key);
            var asset = await assetHandler.GetAsset();

            if (asset != null)
            {
                _assetStorage.Add(assetHandler);
            }

            return (assetHandler.Id, asset);
        }

        public async UniTask<T> GetAsset(string key, string id)
        {
            var assetHandler = _assetStorage.Get(key, id);

            return await assetHandler.GetAsset();
        }

        public void ReleaseAsset(string key, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            
            var assetHandler = _assetStorage.Get(key, id);

            if (assetHandler == null)
            {
                return;
            }
            
            _assetStorage.Remove(assetHandler);
        }
    }
}