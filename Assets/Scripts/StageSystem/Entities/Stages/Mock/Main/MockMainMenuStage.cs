using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.Main.MainMenu;

namespace StageSystem.Entities.Stages.Mock.Main
{
    public class MockMainMenuStage : MockMainStage
    {
        public override string Id => "MainMenu";

        public MockMainMenuStage(List<MockMainMenuSubStage> mainMenuStages)
        {
            Init(mainMenuStages);
        }
    }
}