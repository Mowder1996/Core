using System.Threading;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class ResourceLoadTask : BaseLoadTask, IResultHolder<Object>
    {
        public Object Result { get; private set; }
        
        public ResourceLoadTask(string key) : base(key)
        {
        }

        protected override async UniTask<UniTaskStatus> Loading(string key, 
                                                                ProgressLoadStream progressLoadStream, 
                                                                CancellationToken cancellationToken)
        {
            var loadResource = 
                Addressables.LoadAssetAsync<Object>(key)
                    .ToUniTask(progressLoadStream, 
                                PlayerLoopTiming.Update, 
                                cancellationToken);

            Result = await loadResource;

            return loadResource.Status;
        }
    }
}