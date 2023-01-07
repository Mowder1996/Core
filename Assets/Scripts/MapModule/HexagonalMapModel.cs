namespace MapModule
{
    public class HexagonalMapModel : IMapModel
    {
        public MapTile RootTile { get; }

        public HexagonalMapModel(MapTile rootTile)
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