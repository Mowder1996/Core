using Common.Tests;
using NUnit.Framework;
using StageSystem.Entities.Stages.Mock;
using StageSystem.Entities.Stages.Mock.Main;
using StageSystem.Interfaces;
using UnityEngine;

namespace StageSystem.Tests
{
    public class StageSystemTests : BaseUnitTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            
            Container.Install<MockStageSystemInstaller>();
        }

        [Test]
        public void InitialTest()
        {
            var stageSystem = Container.Resolve<StageSystem>();
            var firstLayerStages = Container.ResolveAll<MockMainStage>();
            
            stageSystem.Init(firstLayerStages);

            var stageHierarchy = stageSystem.GetStageHierarchy("EditAmmoValues");

            var output = "";

            foreach (var stage in stageHierarchy)
            {
                output += stage.Id + "|";
            }

            Debug.Log(output);
        }
    }
}