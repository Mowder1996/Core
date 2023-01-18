namespace MapModule
{
    public interface IMapModel
    {
        public IMapTile GetTileById(string id);
        public bool Validate();
    }
}