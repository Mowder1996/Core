using ContentLoader.Data;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogLoadTask : BaseLoadTask
    {
        public CatalogLoadTask(string key) : base(key)
        {
        }
        
        protected override async UniTask Loading(string key)
        {
            var loadCatalog = 
                Addressables.LoadContentCatalogAsync(key, true)
                            .ToUniTask(ProgressLoadStream, 
                                        PlayerLoopTiming.Update, 
                                        CancellationTokenSource.Token);

            await loadCatalog;

            var status = loadCatalog.Status;

            if (!status.Equals(UniTaskStatus.Canceled))
            {
                SetStatus(status.Equals(UniTaskStatus.Succeeded) ? LoadStatus.Success : LoadStatus.Failed);   
            }
        }
    }
}