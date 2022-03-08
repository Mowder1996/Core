using System;
using System.Threading.Tasks;
using Common.Interfaces;
using ContentLoader.Entities;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ContentLoader.Services
{
    public class CatalogLoaderService : IService
    {
        public UniTask<bool> LoadCatalog(string catalogPath, out ProgressLoadStream progressLoadStream)
        {
            progressLoadStream = new ProgressLoadStream();
            
            return LoadCatalogInternal(catalogPath).ToUniTask(true);
        }

        private IObservable<bool> LoadCatalogInternal(string catalogPath)
        {
            var subject = new Subject<bool>();
            
            var loadCatalog = 
                Addressables.LoadContentCatalogAsync(catalogPath, true);

            loadCatalog.ToObservable()
                .Subscribe(_ =>
                {
                    subject.OnNext(loadCatalog.Status == AsyncOperationStatus.Succeeded);
                    subject.OnCompleted();
                });

            return subject;
        }
    }
}