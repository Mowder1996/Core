using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using StageSystem.Entities.Stages;
using StageSystem.Interfaces;
using StageSystem.Storages;

namespace StageSystem.Services
{
    public class StageOrganizerService : IService
    {
        private readonly StageStorage _stageStorage;

        public StageOrganizerService(StageStorage stageStorage)
        {
            _stageStorage = stageStorage;
        }

        public void Init(IEnumerable<IStage> stages)
        {
            _stageStorage.Clear();
            
            foreach (var stage in stages)
            {
                _stageStorage.Add(stage);
            }
        }

        /// <summary>
        /// Build sequence from first stage to last sub stage of last stage
        /// </summary>
        /// <param name="firstStageId"></param>
        /// <returns>Sequence from first stage to last sub stage of last stage</returns>
        public IEnumerable<IStage> GetStageSequence(string firstStageId)
        {
            var stageHierarchy = GetStageHierarchyInternal(firstStageId).ToList();

            if (!stageHierarchy.Any())
            {
                return null;
            }
            
            stageHierarchy.Reverse();

            var startStage = stageHierarchy.First();
            
            var stageSequence = new List<IStage>();
            stageSequence.AddRange(GetStagesRecursively(startStage));

            for (var i = 0; i < stageHierarchy.Count - 1; i++)
            {
                var stages = GetNextStagesOnSameLevel(stageHierarchy[i], stageHierarchy[i + 1]);

                foreach (var stage in stages)
                {
                    stageSequence.AddRange(GetStagesRecursively(stage));
                }
            }

            return stageSequence;
        }

        /// <summary>
        /// Find required stage and all parents.
        /// </summary>
        /// <param name="stageId"></param>
        /// <param name="sourceStageList"></param>
        /// <returns>Chain of all parents (started with Head's stage) and required stage (last)</returns>
        public IEnumerable<IStage> GetStageHierarchy(string stageId, IEnumerable<IStage> sourceStageList = null)
        {
            return GetStageHierarchyInternal(stageId, sourceStageList);
        }

        private IEnumerable<IStage> GetStageHierarchyInternal(string stageId, IEnumerable<IStage> sourceStageList = null)
        {
            var stagesHierarchy = new List<IStage>();

            if (sourceStageList == null)
            {
                sourceStageList = _stageStorage;

                if (_stageStorage.Count > 1)
                {
                    var customHeadStage = new CustomHeadStage(_stageStorage);
                    
                    stagesHierarchy.Add(customHeadStage);
                }
            }

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

                var subStageHierarchy = GetStageHierarchyInternal(stageId, stage.SubStages).ToList();

                if (!subStageHierarchy.Any())
                {
                    continue;
                }
                    
                stagesHierarchy.Add(stage);
                stagesHierarchy.AddRange(subStageHierarchy);
            }

            return stagesHierarchy;
        }

        private IEnumerable<IStage> GetNextStagesOnSameLevel(IStage startStage, IStage parentStage)
        {
            var subStages = parentStage.SubStages.ToList();

            if (!subStages.Any())
            {
                return subStages;
            }
            
            var startStageIndex = subStages.IndexOf(startStage);

            var nextStages = new List<IStage>();

            for (var i = startStageIndex + 1; i < subStages.Count; i++)
            {
                nextStages.Add(subStages[i]);
            }

            return nextStages;
        }

        private IEnumerable<IStage> GetStagesRecursively(IStage stage)
        {
            var stageSequence = new List<IStage>();
            
            var subStages = stage.SubStages.ToList();
            
            stageSequence.Add(stage);
            
            if (!subStages.Any())
            {
                return stageSequence;
            }

            foreach (var subStage in subStages)
            {
                var nestedSubStages = GetStagesRecursively(subStage);
                
                stageSequence.AddRange(nestedSubStages);
            }

            return stageSequence;
        }
    }
}