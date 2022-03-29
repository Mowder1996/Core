using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogLoadTask : BaseLoadTask
    {
        public CatalogLoadTask(string key) : base(key)
        {
        }
        
        protected override async UniTask<UniTaskStatus> Loading(string key)
        {
            var loadCatalog = 
                Addressables.LoadContentCatalogAsync(key, true)
                            .ToUniTask(ProgressLoadStream, 
                                        PlayerLoopTiming.Update, 
                                        CancellationTokenSource.Token);

            await loadCatalog;

            return loadCatalog.Status;
        }
    }
}