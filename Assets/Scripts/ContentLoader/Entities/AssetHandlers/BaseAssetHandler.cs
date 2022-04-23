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

        protected BaseAssetHandler(TLoadTask loadTask)
        {
            LoadTask = loadTask;
            
            IsLoaded = LoadTask.Status.Equals(LoadStatus.Success);
        }

        public virtual async UniTask Load()
        {
            if (!IsLoaded)
            {
                await LoadTask.Execute();
            }
            
            IsLoaded = LoadTask.Status.Equals(LoadStatus.Success);
        }

        public abstract void Unload();
    }
}