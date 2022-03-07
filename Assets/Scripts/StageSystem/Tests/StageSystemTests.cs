using System.Linq;
using Common.Tests;
using NUnit.Framework;
using StageSystem.Entities;
using StageSystem.Entities.Stages.Mock;
using StageSystem.Entities.Stages.Mock.Main;
using StageSystem.Services;

namespace StageSystem.Tests
{
    public class StageSystemTests : BaseUnitTestFixture
    {
        private StageOrganizerService _stageOrganizerService;
        private StageSystemController _stageSystemController;
        private StageSystemModel _stageSystemModel;

        public override void Setup()
        {
            base.Setup();
            
            Container.Install<MockStageSystemInstaller>();
            
            _stageOrganizerService = Container.Resolve<StageOrganizerService>();
            _stageSystemController = Container.Resolve<StageSystemController>();
            _stageSystemModel = Container.Resolve<StageSystemModel>();
            
            var firstLayerStages = Container.ResolveAll<MockMainStage>();
            
            _stageOrganizerService.Init(firstLayerStages);
        }

        [Test]
        public void StageSequenceIsEmptyIfStartStageNotExists()
        {
            var stageSequence = _stageOrganizerService.GetStageSequence("UndefinedStage");
            
            Assert.IsFalse(stageSequence.Any());
        }
        
        [Test]
        public void ParentStageDontExecuteAfterSubStage()
        {
            var stageHierarchy = _stageOrganizerService.GetStageHierarchy("SelectWeather").ToList();
            var stageSequence = _stageOrganizerService.GetStageSequence("SelectWeather");
            var stageParent = stageHierarchy[stageHierarchy.Count - 2];

            var stageIsExist = false;

            foreach (var stage in stageSequence)
            {
                if (stage.Id.Equals(stageParent.Id))
                {
                    stageIsExist = true;
                    
                    break;
                }
            }
            
            Assert.IsFalse(stageIsExist);
        }
    }
}