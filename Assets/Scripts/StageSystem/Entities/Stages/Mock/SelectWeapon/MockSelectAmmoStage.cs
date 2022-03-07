using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.SelectWeapon.SelectAmmo;

namespace StageSystem.Entities.Stages.Mock.SelectWeapon
{
    public class MockSelectAmmoStage : MockSelectWeaponSubStage
    {
        public override string Id => "SelectAmmo";

        public MockSelectAmmoStage(List<MockSelectAmmoSubStage> mockSelectAmmoSubStages)
        {
            Init(mockSelectAmmoSubStages);
        }
    }
}