using StageSystem.Bootstrap;
using StageSystem.Entities.Stages.Mock.Main;
using StageSystem.Entities.Stages.Mock.Main.MainMenu;
using StageSystem.Entities.Stages.Mock.SelectMap;
using StageSystem.Entities.Stages.Mock.SelectMap.SelectEnvironment;
using StageSystem.Entities.Stages.Mock.SelectPlayer;
using StageSystem.Entities.Stages.Mock.SelectWeapon;
using StageSystem.Entities.Stages.Mock.SelectWeapon.SelectAmmo;

namespace StageSystem.Entities.Stages.Mock
{
    public class MockStageSystemInstaller : StageSystemInstaller
    {
        protected override void InstallStages()
        {
            InstallStage<MockMainStage, MockMainMenuStage>();
            InstallStage<MockMainStage, MockGameStage>();
            
            InstallStage<MockMainMenuSubStage, MockSelectPlayerStage>();
            InstallStage<MockMainMenuSubStage, MockSelectWeaponStage>();
            InstallStage<MockMainMenuSubStage, MockSelectMapStage>();
            
            InstallStage<MockSelectPlayerSubStage, MockSelectPlayerSkinStage>();
            InstallStage<MockSelectPlayerSubStage, MockSelectSkinPatternStage>();
            
            InstallStage<MockSelectWeaponSubStage, MockSelectWeaponTypeStage>();
            InstallStage<MockSelectWeaponSubStage, MockSelectAmmoStage>();
            
            InstallStage<MockSelectAmmoSubStage, MockEditAmmoValuesStage>();
            InstallStage<MockSelectAmmoSubStage, MockEditBoostsStage>();
            
            InstallStage<MockSelectMapSubStage, MockSelectEnvironmentStage>();
            InstallStage<MockSelectMapSubStage, MockSelectWeatherStage>();

            InstallStage<MockSelectEnvironmentSubStage, MockEditBuildingsStage>();
            InstallStage<MockSelectEnvironmentSubStage, MockEditFloraStage>();
        }
    }
}