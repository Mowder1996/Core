namespace MapModule
{
    public class HexagonalCircleMapModel : IMapModel
    {
        public MapTile RootTile { get; }

        public HexagonalCircleMapModel(MapTile rootTile)
        {
            RootTile = rootTile;
        }

        public MapTile GetTileById(string id)
        {
            return RootTile;
        }

        public bool Validate()
        {
            return true;
        }
    }
}