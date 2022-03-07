using System.Collections;
using Common.Tests;
using NUnit.Framework;
using StageSystem.Entities.Stages.Mock;
using StageSystem.Entities.Stages.Mock.Main;
using StageSystem.Services;
using UnityEngine;
using UnityEngine.TestTools;

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
            var stageService = Container.Resolve<StageOrganizerService>();
            var firstLayerStages = Container.ResolveAll<MockMainStage>();
            
            stageSystem.Init(firstLayerStages);
        
            var stageHierarchy = stageService.GetStageSequence("SelectWeapon");
        
            var output = "";
        
            foreach (var stage in stageHierarchy)
            {
                output += stage.Id + "|";
            }
        
            Debug.Log(output);
        }

        [UnityTest]
        public IEnumerator RestartStageTest()
        {
            var stageSystem = Container.Resolve<StageSystem>();
            var firstLayerStages = Container.ResolveAll<MockMainStage>();
            
            stageSystem.Init(firstLayerStages);
            stageSystem.Play("MainMenu");

            yield return null;
            yield return null;
            yield return null;
            
            stageSystem.Play("MainMenu");
        }
    }
}