using System.Collections;
using Common.IntegrationTests;
using ContentLoader.Bootstrap;
using ContentLoader.Services;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ContentLoader.IntegrationTests
{
    public class ContentLoaderIntegrationTests : BaseIntegrationTestFixture
    {
        private CatalogLoaderService _catalogLoaderService;
        private PrefabSpawnerService _prefabSpawnerService;
        
        public override void InstallAndResolveBindings()
        {
            Container.Install<ContentLoaderInstaller>();

            _catalogLoaderService = Container.Resolve<CatalogLoaderService>();
            _prefabSpawnerService = Container.Resolve<PrefabSpawnerService>();
        }

        [UnityTest]
        public IEnumerator InitialTest()
        {
            yield return _catalogLoaderService.LoadCatalog("C:/UnityProjects/Core-Addressables/Windows/catalog.json");

            Addressables.LoadSceneAsync("SampleScene.unity");
            Addressables.LoadSceneAsync("SampleAdditiveScene.unity", LoadSceneMode.Additive);
            
            Assert.IsTrue(SceneManager.GetSceneAt(1).isSubScene);
            Assert.AreEqual(SceneManager.sceneCount, 2);
            
            yield return new WaitForSeconds(3);
        }
    }
}