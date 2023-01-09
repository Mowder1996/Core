using System.Collections.Generic;
using UnityEngine;

namespace MapModule
{
    public class HexagonalCircleMapFactory : IMapFactory
    {
        private const int EdgeAngle = 60;
        private const int EdgeCount = 6;
        
        public IMapModel Create()
        {
            var rootTile = CreateTile(Vector3.zero, Quaternion.identity);
            var baseLayer = new List<MapTile>() { rootTile };

            for (var i = 0; i < 3; i++)
            {
                baseLayer = (List<MapTile>) CreateOuterLayer(baseLayer);
            }
            
            return new HexagonalCircleMapModel(rootTile);
        }

        private IEnumerable<MapTile> CreateOuterLayer(IEnumerable<MapTile> mapLayer)
        {
            var outerLayer = new List<MapTile>();
            
            foreach (var tile in mapLayer)
            {
                var localOuterLayer = CreateOuterLayer(tile);
                
                outerLayer.AddRange(localOuterLayer);
            }

            return outerLayer;
        }

        private IEnumerable<MapTile> CreateOuterLayer(MapTile mapTile)
        {
            var centerVector = 2 * Vector3.forward;

            for (var i = 0; i < EdgeCount; i++)
            {
                if (mapTile.ChainedTiles.ContainsKey(i))
                {
                    continue;
                }
                
                var orientation = mapTile.Orientation * Quaternion.AngleAxis(EdgeAngle * i, Vector3.up);
                var center = mapTile.Center + orientation * centerVector;
                var tile = CreateTile(center, orientation);
                
                tile.AddChainedTile(3, mapTile);
                mapTile.AddChainedTile(i, tile);
            
                var previousIndex = (mapTile.ChainedTiles.Count + (i - 1)) % mapTile.ChainedTiles.Count;
                var nextIndex = (mapTile.ChainedTiles.Count + (i + 1)) % mapTile.ChainedTiles.Count;
            
                if (mapTile.ChainedTiles.ContainsKey(previousIndex))
                {
                    mapTile.ChainedTiles[previousIndex].AddChainedTile(2, tile);
                    tile.AddChainedTile(4, mapTile.ChainedTiles[previousIndex]);
                }
            
                if (mapTile.ChainedTiles.ContainsKey(nextIndex))
                {
                    mapTile.ChainedTiles[nextIndex].AddChainedTile(4, tile);
                    tile.AddChainedTile(2, mapTile.ChainedTiles[nextIndex]);
                }
            }

            return mapTile.ChainedTiles.Values;
        }

        private MapTile CreateTile(Vector3 center, Quaternion orientation)
        {
            var tile = new HexagonalMapTile(center, orientation);

            return tile;
        }
        

    }
}