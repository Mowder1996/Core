using ContentLoader.Data;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Entities.AssetHandlers
{
    public abstract class BaseAssetHandler<T, TLoadTask> : IAssetHandler<T>, ILoadable 
        where T : Object
        where TLoadTask : ILoadTask
    {
        protected readonly TLoadTask LoadTask;

        public bool IsLoaded { get; private set; }
        public T Instance { get; protected set; }
        
        public BaseAssetHandler(TLoadTask loadTask)
        {
            LoadTask = loadTask;
        }

        public virtual async UniTask Load()
        {
            if (IsLoaded)
            {
                return;
            }
            
            await LoadTask.Execute();

            IsLoaded = LoadTask.Status.Equals(LoadStatus.Success);
        }

        public abstract void Unload();
    }
}