using ContentLoader.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class PrefabLoadTask : BaseLoadTask
    {
        public GameObject Result { get; private set; }
        
        public PrefabLoadTask(string key) : base(key)
        {
        }
        
        protected override async UniTask Loading(string key)
        {
            var handle = Addressables.InstantiateAsync(key)
                .ToUniTask(ProgressLoadStream, PlayerLoopTiming.Update, CancellationTokenSource.Token);

            Result = await handle;
            
            var status = handle.Status;

            if (!status.Equals(UniTaskStatus.Canceled))
            {
                SetStatus(status.Equals(UniTaskStatus.Succeeded) ? LoadStatus.Success : LoadStatus.Failed);   
            }
        }
    }
}