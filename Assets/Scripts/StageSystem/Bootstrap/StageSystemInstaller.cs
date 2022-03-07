using Common.Extensions.Zenject;
using StageSystem.Entities;
using StageSystem.Interfaces;
using StageSystem.Services;
using StageSystem.Storages;
using Zenject;

namespace StageSystem.Bootstrap
{
    public abstract class StageSystemInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFacade<StageSystem>();
            
            InstallEntities();
            InstallStorages();
            InstallServices();
        }

        private void InstallEntities()
        {
            Container.InstallModel<StageSystemModel>();

            InstallStages();
        }

        private void InstallStorages()
        {
            Container.InstallStorage<IStage, StageStorage>();
        }
        
        private void InstallServices()
        {
            Container.InstallService<StageOrganizerService>();
            Container.InstallService<StageSystemController>();
        }

        protected abstract void InstallStages();

        protected void InstallStage<TContract, TStage>() 
            where TStage : TContract 
            where TContract : IStage
        {
            Container.Bind<TContract>().To<TStage>().AsSingle();
        }
    }
}