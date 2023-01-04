using System.Threading;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class PrefabLoadTask : BaseLoadTask, IResultHolder<GameObject>
    {
        public GameObject Result { get; private set; }
        
        public PrefabLoadTask(string key) : base(key)
        {
        }
        
        protected override async UniTask<UniTaskStatus> Load(string key, 
                                                                ProgressLoadStream progressLoadStream, 
                                                                CancellationToken cancellationToken)
        {
            var handle = Addressables.InstantiateAsync(key)
                .ToUniTask(progressLoadStream, 
                            PlayerLoopTiming.Update, 
                            cancellationToken);

            Result = await handle;
            
            return handle.Status;
        }
    }
}