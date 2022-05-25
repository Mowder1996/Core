using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ContentLoader.Data;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;
using UniRx;

namespace ContentLoader.Entities.LoadTasks
{
    public class CatalogsUpdateTask : ILoadTask
    {
        private readonly List<CatalogUpdateTask> _updateTasks;
        
        public IObservable<float> ProgressStream { get; }
        public LoadStatus Status => GetStatus();
        public long DownloadSize { get; }
        
        private CancellationTokenSource _cancellationTokenSource;

        public CatalogsUpdateTask(List<CatalogUpdateTask> updateTasks)
        {
            _updateTasks = updateTasks;

            DownloadSize = _updateTasks.Sum(task => task.DownloadSize);

            var progressStream = _updateTasks
                .Select(task => task.ProgressStream.Scan((x, y) => y - x))
                .Merge()
                .Scan((x, y) => (x + y) / updateTasks.Count);

            ProgressStream = progressStream;
        }
        
        public UniTask Execute()
        {
            return UniTask.WhenAll(_updateTasks.Select(task => task.Execute()));
        }

        public void Cancel()
        {
            _updateTasks.ForEach(task => task.Cancel());
        }

        private LoadStatus GetStatus()
        {
            if (_updateTasks == null)
            {
                return LoadStatus.None;
            }

            return _updateTasks.All(task => task.Status.Equals(LoadStatus.Process))
                ? LoadStatus.Process
                : LoadStatus.Success;
        }
    }
}