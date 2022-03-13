using NUnit.Framework;
using Zenject;

namespace Common.IntegrationTests
{
    public abstract class BaseIntegrationTestFixture : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public virtual void Setting()
        {
            SkipInstall();
            
            InstallAndResolveBindings();
        }

        public abstract void InstallAndResolveBindings();
    }
}