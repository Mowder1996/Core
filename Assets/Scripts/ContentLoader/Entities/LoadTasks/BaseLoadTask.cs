using System;
using System.Threading;
using ContentLoader.Data;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;

namespace ContentLoader.Entities.LoadTasks
{
    public abstract class BaseLoadTask : ILoadTask
    {
        public IObservable<float> ProgressStream => ProgressLoadStream;
        public LoadStatus Status { get; private set; } = LoadStatus.None;

        protected CancellationTokenSource CancellationTokenSource;
        protected ProgressLoadStream ProgressLoadStream;
        
        public virtual UniTask Execute(string key)
        {
            SetStatus(LoadStatus.Process);
            
            ProgressLoadStream = new ProgressLoadStream();
            CancellationTokenSource = new CancellationTokenSource();

            return Loading(key);
        }

        public void Cancel()
        {
            if (Status == LoadStatus.Process)
            {
                SetStatus(LoadStatus.Cancelled);
            }

            ProgressLoadStream = null;
            CancellationTokenSource.Cancel();
            CancellationTokenSource.Dispose();
        }

        protected abstract UniTask Loading(string key);

        protected void SetStatus(LoadStatus status)
        {
            Status = status;
        }
    }
}