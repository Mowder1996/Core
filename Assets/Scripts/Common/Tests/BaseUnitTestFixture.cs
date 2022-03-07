using Zenject;

namespace Common.Tests
{
    public class BaseUnitTestFixture : ZenjectUnitTestFixture
    {
        public override void Setup()
        {
            base.Setup();
            
            SignalBusInstaller.Install(Container);
        }
    }
}