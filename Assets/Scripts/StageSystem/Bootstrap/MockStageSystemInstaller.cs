using StageSystem.Entities.Stages;
using StageSystem.Interfaces;

namespace StageSystem.Bootstrap
{
    public class MockStageSystemInstaller : StageSystemInstaller
    {
        protected override void InstallStages()
        {
            base.InstallStages();

            Container.Bind<IStage>().WithId("First layer").To<MockFirstLayerStage>().AsSingle();
            Container.Bind<IStage>().WithId("Second layer").To<MockSecondLayerStage>().AsSingle();
            Container.Bind<IStage>().WithId("Third layer").To<MockThirdLayerStage>().AsSingle();
            Container.Bind<IStage>().WithId("Fourth layer").To<MockFourthLayerStage>().AsSingle();
        }
    }
}