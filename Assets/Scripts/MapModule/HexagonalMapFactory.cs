using System.Collections.Generic;
using UnityEngine;

namespace MapModule
{
    public class HexagonalMapFactory : IMapFactory
    {
        private const float Cos30Const = 0.866f;
        private const int EdgeAngle = 60;
        
        public IMapModel Create()
        {
            var rootTile = CreateTile(Vector3.zero, Quaternion.identity);
            var centerVector = Vector3.forward;
            
            for (var i = 0; i < 6; i++)
            {
                if (rootTile.ChainedTiles.ContainsKey(i))
                {
                    continue;
                }
                
                var orientation = Quaternion.AngleAxis(EdgeAngle * i, Vector3.up);
                var center = orientation * (2 * centerVector);
                var tile = CreateTile(center, orientation);
                tile.AddChainedTile(3, rootTile);
                
                rootTile.AddChainedTile(i, tile);

                var previousIndex = (rootTile.ChainedTiles.Count + (i - 1)) % rootTile.ChainedTiles.Count;
                var nextIndex = (rootTile.ChainedTiles.Count + (i + 1)) % rootTile.ChainedTiles.Count;

                if (rootTile.ChainedTiles.ContainsKey(previousIndex))
                {
                    rootTile.ChainedTiles[previousIndex].AddChainedTile(2, tile);
                    tile.AddChainedTile(4, rootTile.ChainedTiles[previousIndex]);
                }

                if (rootTile.ChainedTiles.ContainsKey(nextIndex))
                {
                    rootTile.ChainedTiles[nextIndex].AddChainedTile(4, tile);
                    tile.AddChainedTile(2, rootTile.ChainedTiles[nextIndex]);
                }
            }
            
            return new HexagonalMapModel(rootTile);
        }

        private MapTile CreateTile(Vector3 center, Quaternion orientation)
        {
            var bounds = CreateBounds(center, orientation, 1);
            var tile = new MapTile(center, bounds);

            return tile;
        }
        
        private List<Vector3> CreateBounds(Vector3 center, Quaternion orientation, float size)
        {
            var bounds = new List<Vector3>();
            
            var pointVector = size / Cos30Const * Vector3.forward;
            var pointOrientation = orientation * Quaternion.AngleAxis(EdgeAngle / 2f, Vector3.up);
            var rotateQuaternion = Quaternion.AngleAxis(EdgeAngle, Vector3.up);

            for (var i = 0; i < 6; i++)
            {
                bounds.Add(center + pointOrientation * pointVector);

                pointOrientation *= rotateQuaternion;
            }

            return bounds;
        }
    }
}