using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader
{
    public class ContentLoader
    {
        private readonly AssetServiceResolver _assetServiceResolver;

        public ContentLoader(AssetServiceResolver assetServiceResolver)
        {
            _assetServiceResolver = assetServiceResolver;
        }
        
        public async UniTask<IEnumerable<string>> GetKeysByLabels(IEnumerable<string> labels, Addressables.MergeMode mergeMode)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(labels, mergeMode);

            return locations.Select(item => item.PrimaryKey);
        }
        
        public async UniTask<IEnumerable<string>> GetKeysByLabels<T>(IEnumerable<string> labels, Addressables.MergeMode mergeMode)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(labels, mergeMode, typeof(T));

            return locations.Select(item => item.PrimaryKey);
        }

        /// <summary>
        /// Grab new asset from loaded catalogs
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Tuple type (Id : string, Asset : T)</returns>
        public async UniTask<(string, T)> CreateAsset<T>(string key) where T : Object
        {
            var service = GetAssetService<T>();

            return await service.CreateAsset(key);
        }

        public async UniTask<T> GetAsset<T>(string key, string id) where T : Object
        {
            var service = GetAssetService<T>();

            return await service.GetAsset(key, id);
        }

        public void ReleaseAsset<T>(string key, string id) where T : Object
        {
            var service = GetAssetService<T>();
            
            service.ReleaseAsset(key, id);
        }

        private AssetService<T> GetAssetService<T>() where T : Object
        {
            return _assetServiceResolver.Resolve<T>();
        }
    }
}
