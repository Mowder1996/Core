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

        protected ProgressLoadStream ProgressLoadStream;
        protected CancellationTokenSource CancellationTokenSource;

        private bool _isInitialized;

        protected BaseLoadTask()
        {
            Initialize();
        }
        
        ~BaseLoadTask()
        {
            Dispose();
        }

        public virtual async UniTask Execute(string key)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            SetStatus(LoadStatus.Process);
            
            await Loading(key);
            
            Dispose();
        }

        public void Cancel()
        {
            if (Status == LoadStatus.Process)
            {
                SetStatus(LoadStatus.Cancelled);
            }
            
            Dispose();
        }

        protected abstract UniTask Loading(string key);

        protected void SetStatus(LoadStatus status)
        {
            Status = status;
        }

        private void Initialize()
        {
            ProgressLoadStream = new ProgressLoadStream();
            CancellationTokenSource = new CancellationTokenSource();

            _isInitialized = true;
        }
        
        private void Dispose()
        {
            _isInitialized = false;
            
            ProgressLoadStream?.Dispose();
            
            CancellationTokenSource.Cancel();
            CancellationTokenSource?.Dispose();
        }
    }
}