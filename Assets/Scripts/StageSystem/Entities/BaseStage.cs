using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using StageSystem.Interfaces;

namespace StageSystem.Entities
{
    public class BaseStage : IStage, IDisposable
    {
        public virtual string Id => "DefaultStage";

        private List<IStage> _subStages = new List<IStage>();
        public IEnumerable<IStage> SubStages => _subStages;

        protected CancellationTokenSource CancellationTokenSource;

        protected void Init(IEnumerable<IStage> subStages)
        {
            _subStages.Clear();
            _subStages.AddRange(subStages);
        }
        
        public async UniTask Execute()
        {
            CancellationTokenSource = new CancellationTokenSource();
            
            await UniTask.Yield();
        }

        public void Skip()
        {
            CancellationTokenSource.Cancel();
        }

        public void Dispose()
        {
            CancellationTokenSource?.Dispose();
        }
    }
}