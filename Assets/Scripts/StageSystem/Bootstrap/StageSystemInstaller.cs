using StageSystem.Entities;
using StageSystem.Interfaces;
using Zenject;

namespace StageSystem.Bootstrap
{
    public class StageSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallEntities();
            InstallStages();
        }

        private void InstallEntities()
        {
            Container.Bind<StageSystem>().AsSingle();
            Container.Bind<StageInventory>().AsSingle();
            Container.Bind<StageSystemModel>().AsSingle();
        }

        protected virtual void InstallStages()
        {
            
        }

        protected void InstallStage<TContract, TStage>() 
            where TStage : TContract 
            where TContract : IStage
        {
            Container.Bind<TContract>().To<TStage>().AsSingle();
        }
    }
}