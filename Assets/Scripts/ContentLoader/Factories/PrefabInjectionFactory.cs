using UnityEngine;
using Zenject;

namespace ContentLoader.Factories
{
    public class PrefabInjectionFactory : PlaceholderFactory<GameObject, GameObject>
    {
        private readonly DiContainer _diContainer;

        public PrefabInjectionFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public override GameObject Create(GameObject prefab)
        {
            _diContainer.InjectGameObject(prefab);

            return prefab;
        }

        public TComponent CreateForComponent<TComponent>(GameObject prefab) where TComponent : Component
        {
            return _diContainer.InjectGameObjectForComponent<TComponent>(prefab);
        }
    }
}