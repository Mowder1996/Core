using System;
using System.Threading;
using Common.Interfaces;
using ContentLoader.Data;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ContentLoader.Entities.LoadTasks
{
    public abstract class BaseLoadTask : ILoadTask, IIdentified
    {
        public string Id { get; }
        public IObservable<float> ProgressStream => _progressLoadStream;
        public LoadStatus Status { get; private set; } = LoadStatus.None;
        public long DownloadSize { get; private set; }

        private ProgressLoadStream _progressLoadStream;
        private CancellationTokenSource _cancellationTokenSource;

        protected BaseLoadTask(string key)
        {
            Id = key;
        }
        
        private void Initialize()
        {
            _progressLoadStream = new ProgressLoadStream();
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        private void Dispose()
        {
            _progressLoadStream?.Dispose();
            
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource?.Dispose();
        }
        
        ~BaseLoadTask()
        {
            Dispose();
        }

        public async UniTask Execute()
        {
            Initialize();

            SetStatus(LoadStatus.Process);

            await GetDownloadSize(Id);
            
            var status = await Loading(Id, _progressLoadStream, _cancellationTokenSource.Token);

            if (!status.Equals(UniTaskStatus.Canceled))
            {
                SetStatus(status.Equals(UniTaskStatus.Succeeded) ? LoadStatus.Success : LoadStatus.Failed);   
            }
            
            Dispose();
        }

        public void Cancel()
        {
            if (Status.Equals(LoadStatus.Process))
            {
                SetStatus(LoadStatus.Cancelled);
            }
            
            Dispose();
        }

        protected abstract UniTask<UniTaskStatus> Loading(string key, 
                                                        ProgressLoadStream progressLoadStream, 
                                                        CancellationToken cancellationToken);

        private async UniTask GetDownloadSize(string key)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(key);

            await sizeHandle;

            DownloadSize = sizeHandle.Status == AsyncOperationStatus.Succeeded ? sizeHandle.Result : 0;
        }
        
        private void SetStatus(LoadStatus status)
        {
            Status = status;
        }
    }
}