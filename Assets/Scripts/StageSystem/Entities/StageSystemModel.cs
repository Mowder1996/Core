using Common.Interfaces;
using StageSystem.Data;
using StageSystem.Interfaces;
using UniRx;
using UnityEngine;

namespace StageSystem.Entities
{
    public class StageSystemModel : IModel
    {
        private readonly ReactiveProperty<IStage> _currentStage = new ReactiveProperty<IStage>();
        public IReadOnlyReactiveProperty<IStage> CurrentStage => _currentStage;
        public StageSequenceStatus SequenceStatus { get; private set; } = StageSequenceStatus.Ready;

        public void SetCurrentStage(IStage stage)
        {
            _currentStage.Value = stage;
        }

        public void SetSequenceStatus(StageSequenceStatus status)
        {
            SequenceStatus = status;
        }
    }
}