using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.SelectMap;

namespace StageSystem.Entities.Stages.Mock.Main.MainMenu
{
    public class MockSelectMapStage : MockMainMenuSubStage
    {
        public override string Id => "SelectMap";
        
        public MockSelectMapStage(List<MockSelectMapSubStage> selectMapSubStages)
        {
            Init(selectMapSubStages);
        }
    }
}