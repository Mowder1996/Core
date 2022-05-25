using ContentLoader.Entities.AssetHandlers;
using ContentLoader.Entities.LoadTasks;
using UnityEngine;
using Zenject;

namespace ContentLoader.Factories
{
    public class PrefabHandlerFactory : PlaceholderFactory<PrefabLoadTask, PrefabHandler>
    {
    }
}