using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.SelectPlayer;

namespace StageSystem.Entities.Stages.Mock.Main.MainMenu
{
    public class MockSelectPlayerStage : MockMainMenuSubStage
    {
        public override string Id => "SelectPlayer";
        
        public MockSelectPlayerStage(List<MockSelectPlayerSubStage> selectPlayerSubStages)
        {
            Init(selectPlayerSubStages);
        }
    }
}