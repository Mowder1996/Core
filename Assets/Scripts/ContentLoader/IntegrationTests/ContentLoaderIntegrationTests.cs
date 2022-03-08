using System.Collections;
using ContentLoader.Bootstrap;
using ContentLoader.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace ContentLoader.IntegrationTests
{
    public class ContentLoaderIntegrationTests : ZenjectIntegrationTestFixture
    {
        private CatalogLoaderService _catalogLoaderService;
        
        [SetUp]
        public void Init()
        {
            SkipInstall();
            
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