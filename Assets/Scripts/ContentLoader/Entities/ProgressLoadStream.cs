using System;
using UniRx;
using UnityEngine;

namespace ContentLoader.Entities
{
    public class ProgressLoadStream : IProgress<float>, IObservable<float>, IDisposable
    {
        private readonly Subject<float> _progressLoadSubject;

        public ProgressLoadStream()
        {
            _progressLoadSubject = new Subject<float>();
        }

        public void Report(float value)
        {
            _progressLoadSubject.OnNext(value);

            var isLoadDone = Mathf.FloorToInt(value) == 1;
                
            if (isLoadDone)
            {
                _progressLoadSubject.OnCompleted();
            }
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return _progressLoadSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            _progressLoadSubject.OnCompleted();
            _progressLoadSubject?.Dispose();
        }
    }
}