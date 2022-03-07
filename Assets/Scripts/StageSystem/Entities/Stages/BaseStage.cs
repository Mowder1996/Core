using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using StageSystem.Interfaces;

namespace StageSystem.Entities.Stages
{
    public class BaseStage : IStage, IDisposable
    {
        private readonly List<IStage> _subStages = new List<IStage>();
        public IEnumerable<IStage> SubStages => _subStages;
        
        public virtual string Id => "UndefinedStage";

        protected CancellationTokenSource CancellationTokenSource;

        protected void Init(IEnumerable<IStage> subStages)
        {
            _subStages.Clear();
            _subStages.AddRange(subStages);
        }
        
        public async UniTask Execute()
        {
            CancellationTokenSource = new CancellationTokenSource();
            
            await UniTask.Yield(CancellationTokenSource.Token);
        }

        public void Skip()
        {
            CancellationTokenSource.Cancel();
            CancellationTokenSource?.Dispose();
        }

        public void Dispose()
        {
            CancellationTokenSource?.Dispose();
        }
    }
}