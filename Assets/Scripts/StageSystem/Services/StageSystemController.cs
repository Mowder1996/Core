using System.Collections.Generic;
using Common.Interfaces;
using Cysharp.Threading.Tasks;
using StageSystem.Data;
using StageSystem.Entities;
using StageSystem.Interfaces;

namespace StageSystem.Services
{
    public class StageSystemController : IService
    {
        private readonly StageSystemModel _stageSystemModel;

        public StageSystemController(StageSystemModel stageSystemModel)
        {
            _stageSystemModel = stageSystemModel;
        }

        public async void PlaySequence(IEnumerable<IStage> stageSequence)
        {
            while (!_stageSystemModel.SequenceStatus.Equals(StageSequenceStatus.Ready))
            {
                await UniTask.Yield();
            }

            await PlaySequenceInternal(stageSequence);
        }

        public void StopSequence()
        {
            if (!_stageSystemModel.SequenceStatus.Equals(StageSequenceStatus.Process))
            {
                return;
            }
            
            _stageSystemModel.SetSequenceStatus(StageSequenceStatus.Cancelled);  
            _stageSystemModel.CurrentStage?.Value?.Skip();
        }

        private async UniTask PlaySequenceInternal(IEnumerable<IStage> stageSequence)
        {
            _stageSystemModel.SetSequenceStatus(StageSequenceStatus.Process);
            
            foreach (var stage in stageSequence)
            {
                await PlayStage(stage);

                if (_stageSystemModel.SequenceStatus.Equals(StageSequenceStatus.Cancelled))
                {
                    break;
                }
            }
            
            _stageSystemModel.SetSequenceStatus(StageSequenceStatus.Ready);
        }
        
        private UniTask PlayStage(IStage stage)
        {
            _stageSystemModel.SetCurrentStage(stage);
            
            return stage.Execute().SuppressCancellationThrow();
        }
    }
}