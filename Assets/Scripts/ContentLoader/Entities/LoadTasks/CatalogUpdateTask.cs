using ContentLoader.Data;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogUpdateTask : BaseLoadTask
    {
        public CatalogUpdateTask(string key) : base(key)
        {
        }

        protected override async UniTask Loading(string key)
        {
            var updateCatalog = 
                Addressables.UpdateCatalogs(new []{key}, true)
                    .ToUniTask(ProgressLoadStream, 
                        PlayerLoopTiming.Update, 
                        CancellationTokenSource.Token);

            await updateCatalog;

            var status = updateCatalog.Status;

            if (!status.Equals(UniTaskStatus.Canceled))
            {
                SetStatus(status.Equals(UniTaskStatus.Succeeded) ? LoadStatus.Success : LoadStatus.Failed);   
            }
        }
    }
}