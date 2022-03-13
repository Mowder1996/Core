using System.Collections;
using Common.IntegrationTests;
using ContentLoader.Bootstrap;
using ContentLoader.Services;
using Cysharp.Threading.Tasks;
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
            
            yield return new WaitForSeconds(3);
        }

        [UnityTest]
        public IEnumerator SpawnPrefabTest()
        {
            var task = _prefabSpawnerService.SpawnPrefabFromFactory("Cube").AsTask();
            yield return task;

            Debug.Log($"GO name: {task.Result.Instance.name}");

            var task2 = _prefabSpawnerService.SpawnPrefabFromFactory<MeshFilter>("Cube").AsTask();
            
            yield return task2;
            
            Debug.Log($"Vertices count: {task2.Result.Instance.mesh.vertices.Length}");
            
            yield return new WaitForSeconds(3);
            
            task.Result.Unload();
            task2.Result.Unload();
            
            yield return new WaitForSeconds(3);
        }
    }
}