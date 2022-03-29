using Cysharp.Threading.Tasks;

namespace ContentLoader.Interfaces
{
    public interface ILoadable
    {
        UniTask Load();
        void Unload();
    }
}