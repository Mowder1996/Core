using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace ContentLoader
{
    public class AssetFactory<T> : PlaceholderFactory<string, AssetHandler<T>> where T : Object
    {
        public override AssetHandler<T> Create(string key)
        {
            var operationHandle = Addressables.LoadAssetAsync<T>(key);
            
            return new AssetHandler<T>(key, operationHandle);
        }
    }
}