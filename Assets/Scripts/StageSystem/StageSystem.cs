using System.Collections.Generic;
using StageSystem.Entities;
using StageSystem.Interfaces;

namespace StageSystem
{
    public class StageSystem
    {
        private readonly StageInventory _stageInventory;
        private readonly StageSystemModel _stageSystemModel;

        public StageSystem(StageInventory stageInventory, StageSystemModel stageSystemModel)
        {
            _stageInventory = stageInventory;
            _stageSystemModel = stageSystemModel;
        }

        public void Init(IEnumerable<IStage> stages)
        {
            foreach (var stage in stages)
            {
                _stageInventory.Add(stage);
            }
        }
        
        public void StartStage(string stageId)
        {

        }

        public void SkipCurrentStage(bool skipSubStageOnly = false)
        {
            
        }

        public IEnumerable<IStage> GetStageHierarchy(string stageId)
        {
            return _stageSystemModel.GetStageHierarchy(stageId);
        }
    }
}