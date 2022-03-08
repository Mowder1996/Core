using System.Collections;
using Common.Interfaces;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace ContentLoader
{
    public class ContentLoader : IFacade
    {
        private IEnumerator LoadContent()
        {
            var loadContent = Addressables.LoadContentCatalogAsync(
                "C:/UnityProjects/Core-Addressables/Windows/catalog.json", true);
            
            yield return loadContent;

            if (loadContent.Status != AsyncOperationStatus.Succeeded)
            {
                yield break;
            }

            Addressables.LoadSceneAsync("SampleScene.unity");
            Addressables.LoadSceneAsync("SampleAdditiveScene.unity", LoadSceneMode.Additive);
        }
    }
}
