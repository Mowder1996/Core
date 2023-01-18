using UnityEngine;

namespace MapModule
{
    public class HexagonalMapTile : BaseMapTile
    {
        private readonly string[] _chainedTileIds = new string[6];
        
        private Quaternion _orientation;

        public Quaternion Orientation => _orientation;

        public void AddChainedTile(int index, HexagonalMapTile mapTile)
        {
            if (HasChainedTileInternal(index))
            {
                return;
            }

            _chainedTileIds[index] = mapTile.Id;
            
            base.AddChainedTile(mapTile);
        }

        public void SetOrientation(Quaternion orientation)
        {
            _orientation = orientation;
        }

        public HexagonalMapTile GetTileByIndex(int index)
        {
            if (!HasChainedTileInternal(index))
            {
                return null;
            }

            var tileId = _chainedTileIds[index];

            return (HexagonalMapTile)GetTileById(tileId);
        }
        
        public bool HasChainedTile(int index)
        {
            return HasChainedTileInternal(index);
        }

        private bool HasChainedTileInternal(int index)
        {
            var isIndexCorrect = index >= 0 && index < _chainedTileIds.Length;
            
            return isIndexCorrect && !string.IsNullOrEmpty(_chainedTileIds[index]);
        }
    }
}