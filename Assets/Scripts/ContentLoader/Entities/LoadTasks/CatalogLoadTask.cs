using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogLoadTask : BaseLoadTask
    {
        public CatalogLoadTask(string key) : base(key)
        {
        }
        
        protected override async UniTask<UniTaskStatus> Load(string key, 
                                                                ProgressLoadStream progressLoadStream, 
                                                                CancellationToken cancellationToken)
        {
            var loadCatalog = 
                Addressables.LoadContentCatalogAsync(key, true)
                            .ToUniTask(progressLoadStream, 
                                        PlayerLoopTiming.Update, 
                                        cancellationToken);

            await loadCatalog;

            return loadCatalog.Status;
        }
    }
}