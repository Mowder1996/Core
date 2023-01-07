using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContentLoader
{
    public class AssetStorage<T> where T : Object
    {
        private readonly Dictionary<string, List<AssetHandler<T>>> _assets = new Dictionary<string, List<AssetHandler<T>>>();

        public void Add(AssetHandler<T> assetHandler)
        {
            if (!_assets.ContainsKey(assetHandler.Key))
            {
                _assets.Add(assetHandler.Key, new List<AssetHandler<T>>());
            }

            if (_assets[assetHandler.Key].Any(item => item.Id.Equals(assetHandler.Id)))
            {
                return;
            }
            
            _assets[assetHandler.Key].Add(assetHandler);
        }

        public void Remove(AssetHandler<T> assetHandler)
        {
            if (!_assets.ContainsKey(assetHandler.Key))
            {
                return;
            }

            _assets[assetHandler.Key].Remove(assetHandler);
        }

        public AssetHandler<T> Get(string key, string id)
        {
            if (!_assets.ContainsKey(key))
            {
                return null;
            }

            return _assets[key].FirstOrDefault(item => item.Id.Equals(id));
        }
    }
}