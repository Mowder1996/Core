using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogUpdateTask : BaseLoadTask
    {
        public CatalogUpdateTask(string key) : base(key)
        {
        }

        protected override async UniTask<UniTaskStatus> Load(string key, 
                                                                ProgressLoadStream progressLoadStream, 
                                                                CancellationToken cancellationToken)
        {
            var updateCatalog = 
                Addressables.UpdateCatalogs(new []{key}, true)
                    .ToUniTask(progressLoadStream, 
                                PlayerLoopTiming.Update, 
                                cancellationToken);

            await updateCatalog;

            return updateCatalog.Status;
        }
    }
}