using System.Collections.Generic;
using System.Linq;
using StageSystem.Interfaces;
using UniRx;

namespace StageSystem.Entities
{
    public class StageSystemModel
    {
        private readonly StageInventory _stageInventory;

        private ReactiveProperty<IStage> _currentStage = new ReactiveProperty<IStage>();
        
        public IReadOnlyReactiveProperty<IStage> CurrentStage => _currentStage;
        
        public StageSystemModel(StageInventory stageInventory)
        {
            _stageInventory = stageInventory;
        }

        public void PlayStage(string stageId)
        {
            
        }
        
        private void SetCurrentStage(IStage stage)
        {
            _currentStage.Value = stage;
        }

        private void ResetCurrentStage()
        {
            _currentStage.Value = null;
        }

        private IStage GetParentStage(string stageId)
        {
            return null;
        }
        
        /// <summary>
        /// Find required stage and all parents.
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns>Chain of all parents (started with Head's stage) and required stage (last)</returns>
        public IEnumerable<IStage> GetStageHierarchy(string stageId, IEnumerable<IStage> sourceStageList = null)
        {
            var stagesHierarchy = new List<IStage>();

            sourceStageList ??= _stageInventory;

            foreach (var stage in sourceStageList)
            {
                if (stage == null)
                {
                    continue;
                }

                if (stage.Id.Equals(stageId))
                {
                    stagesHierarchy.Add(stage);
                    
                    break;
                }

                var subStageHierarchy = GetStageHierarchy(stageId, stage.SubStages).ToList();

                if (!subStageHierarchy.Any())
                {
                    continue;
                }
                    
                stagesHierarchy.Add(stage);
                stagesHierarchy.AddRange(subStageHierarchy);
            }

            return stagesHierarchy;
        }
    }
}