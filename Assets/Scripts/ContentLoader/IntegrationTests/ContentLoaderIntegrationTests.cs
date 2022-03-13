using System.Collections;
using Common.IntegrationTests;
using ContentLoader.Bootstrap;
using ContentLoader.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ContentLoader.IntegrationTests
{
    public class ContentLoaderIntegrationTests : BaseIntegrationTestFixture
    {
        private CatalogLoaderService _catalogLoaderService;
        
        public override void InstallAndResolveBindings()
        {
            Container.Install<ContentLoaderInstaller>();

            _catalogLoaderService = Container.Resolve<CatalogLoaderService>();
        }

        [UnityTest]
        public IEnumerator InitialTest()
        {
            yield return _catalogLoaderService.LoadCatalog("C:/UnityProjects/Core-Addressables/Windows/catalog.json");

            Addressables.LoadSceneAsync("SampleScene.unity");
            Addressables.LoadSceneAsync("SampleAdditiveScene.unity", LoadSceneMode.Additive);
            
            yield return new WaitForSeconds(3);
        }
    }
}