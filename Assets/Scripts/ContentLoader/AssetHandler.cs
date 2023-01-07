using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace ContentLoader
{
    public class AssetHandler<T> : IDisposable where T : Object
    {
        public string Id { get; }
        public string Key { get; }
        public AsyncOperationHandle<T> OperationHandle { get; }

        private T _asset;
        
        public AssetHandler(string key, AsyncOperationHandle<T> operationHandle)
        {
            Id = Guid.NewGuid().ToString();
            Key = key;
            OperationHandle = operationHandle;
        }

        public async UniTask<T> GetAsset()
        {
            if (_asset != null)
            {
                return _asset;
            }

            await OperationHandle;

            if (OperationHandle.OperationException != null)
            {
                Debug.LogError($"Assets with key {Key} is not loaded! Message: {OperationHandle.OperationException.Message} StackTrace: {OperationHandle.OperationException.StackTrace}");

                return null;
            }
            
            _asset = OperationHandle.Result;

            return _asset;
        }

        public void Dispose()
        {
            _asset = null;
        }

        public override bool Equals(object obj)
        {
            var concreteObj = (AssetHandler<T>) obj;

            if (concreteObj == null)
            {
                return false;
            }

            return concreteObj.Id.Equals(Id) && concreteObj.Key.Equals(Key);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id.GetHashCode(), Key.GetHashCode());
        }
    }
}