namespace MapModule
{
    public class HexagonalCircleMapModel : IMapModel
    {
        public IMapTile RootTile { get; }

        public HexagonalCircleMapModel(IMapTile rootTile)
        {
            RootTile = rootTile;
        }

        public IMapTile GetTileById(string id)
        {
            return RootTile;
        }

        public bool Validate()
        {
            return true;
        }
    }
}