using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ContentLoader.Interfaces
{
    public interface IAssetHandler<T>
    {
        bool IsLoaded { get; }
        T Instance { get; }
        UniTask Load();
        void Unload();
    }
}