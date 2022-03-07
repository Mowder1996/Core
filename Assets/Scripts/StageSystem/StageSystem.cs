using System;
using System.Collections.Generic;
using Common.Interfaces;
using StageSystem.Entities;
using StageSystem.Interfaces;
using StageSystem.Services;

namespace StageSystem
{
    public class StageSystem : IFacade
    {
        private readonly StageOrganizerService _stageOrganizerService;
        private readonly StageSystemController _stageSystemController;
        private readonly StageSystemModel _stageSystemModel;

        public StageSystem(StageOrganizerService stageOrganizerService, 
                            StageSystemController stageSystemController, 
                            StageSystemModel stageSystemModel)
        {
            _stageOrganizerService = stageOrganizerService;
            _stageSystemController = stageSystemController;
            _stageSystemModel = stageSystemModel;
        }

        public void Init(IEnumerable<IStage> stages)
        {
            _stageOrganizerService.Init(stages);
        }

        public IObservable<IStage> ChangeStageAsObservable()
        {
            return _stageSystemModel.CurrentStage;
        }

        public void Play(string startStageId)
        {
            _stageSystemController.StopSequence();
            
            var stageSequence = _stageOrganizerService.GetStageSequence(startStageId);

            _stageSystemController.PlaySequence(stageSequence);
        }

        public void Stop()
        {
            _stageSystemController.StopSequence();
        }
    }
}