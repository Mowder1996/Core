namespace MapModule
{
    public interface IMapModel
    {
        public MapTile GetTileById(string id);
        public bool Validate();
    }
}