
namespace ContentLoader.Interfaces
{
    public interface IAssetHandler<T>
    {
        T Instance { get; }
    }
}