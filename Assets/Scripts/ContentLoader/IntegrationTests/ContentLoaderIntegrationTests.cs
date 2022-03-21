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

        [UnityTest]
        public IEnumerator SpawnPrefabTest()
        {
            var task = _prefabSpawnerService.SpawnPrefab("Cube").AsTask();
            yield return task;

            Assert.IsNotNull(task.Result.Instance);

            var task2 = _prefabSpawnerService.SpawnPrefab<MeshFilter>("Cube").AsTask();
            yield return task2;
            
            Assert.IsNotNull(task2.Result.Instance);
            Assert.IsInstanceOf<MeshFilter>(task2.Result);
            
            yield return new WaitForSeconds(3);
            
            task.Result.Unload();
            task2.Result.Unload();
            
            yield return new WaitForSeconds(1);
            
            Assert.IsNull(task.Result);
            Assert.IsNull(task2.Result);
            
            yield return new WaitForSeconds(3);
        }
    }
}