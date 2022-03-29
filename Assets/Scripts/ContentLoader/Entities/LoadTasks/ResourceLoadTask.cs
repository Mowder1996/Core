using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class ResourceLoadTask : BaseLoadTask
    {
        public Object Result { get; private set; }
        
        public ResourceLoadTask(string key) : base(key)
        {
        }

        protected override async UniTask<UniTaskStatus> Loading(string key)
        {
            var loadResource = 
                Addressables.LoadAssetAsync<Object>(key)
                    .ToUniTask(ProgressLoadStream, 
                        PlayerLoopTiming.Update, 
                        CancellationTokenSource.Token);

            Result = await loadResource;

            return loadResource.Status;
        }
    }
}