using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.SelectWeapon;

namespace StageSystem.Entities.Stages.Mock.Main.MainMenu
{
    public class MockSelectWeaponStage : MockMainMenuSubStage
    {
        public override string Id => "SelectWeapon";
        
        public MockSelectWeaponStage(List<MockSelectWeaponSubStage> selectWeaponSubStages)
        {
            Init(selectWeaponSubStages);
        }
    }
}