using ContentLoader.Data;
using ContentLoader.Entities.LoadTasks;
using ContentLoader.Factories;
using ContentLoader.Interfaces;
using Cysharp.Threading.Tasks;

namespace ContentLoader.Entities.AssetHandlers
{
    public abstract class BasePrefabHandler<T> : IAssetHandler<T>
    {
        protected readonly PrefabLoadTask LoadTask;
        protected PrefabInjectionFactory PrefabInjectionFactory { get; private set; }

        public bool IsLoaded { get; private set; }
        public T Instance { get; protected set; }
        
        public BasePrefabHandler(PrefabLoadTask loadTask, PrefabInjectionFactory prefabInjectionFactory)
        {
            LoadTask = loadTask;
            PrefabInjectionFactory = prefabInjectionFactory;
        }

        public virtual async UniTask Load()
        {
            if (IsLoaded)
            {
                return;
            }
            
            await LoadTask.Execute();

            if (LoadTask.Result == null)
            {
                return;
            }
            
            IsLoaded = LoadTask.Status == LoadStatus.Success;
        }

        public abstract void Unload();
    }
}