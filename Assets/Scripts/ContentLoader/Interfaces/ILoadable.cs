using Cysharp.Threading.Tasks;

namespace ContentLoader.Interfaces
{
    public interface ILoadable
    {
        bool IsLoaded { get; }
        UniTask Load();
        void Unload();
    }
}