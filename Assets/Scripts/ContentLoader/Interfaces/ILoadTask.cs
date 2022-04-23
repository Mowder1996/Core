using System;
using ContentLoader.Data;
using Cysharp.Threading.Tasks;

namespace ContentLoader.Interfaces
{
    public interface ILoadTask
    {
        IObservable<float> ProgressStream { get; }
        LoadStatus Status { get; }
        long DownloadSize { get; }
        UniTask Execute();
        void Cancel();
    }
}