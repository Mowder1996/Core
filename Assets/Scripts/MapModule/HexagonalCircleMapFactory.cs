using System.Collections.Generic;
using UnityEngine;

namespace MapModule
{
    public class HexagonalCircleMapFactory : IMapFactory
    {
        private const float Cos30Const = 0.866f;
        private const int EdgeAngle = 60;
        private const int EdgeCount = 6;
        
        public virtual IMapModel Create()
        {
            var rootTile = CreateTile(Vector3.zero, Quaternion.identity);
            var baseLayer = new List<HexagonalMapTile>() { rootTile };

            for (var i = 0; i < 3; i++)
            {
                baseLayer = CreateSurroundTiles(baseLayer);
            }
            
            return new HexagonalCircleMapModel(rootTile);
        }

        private List<HexagonalMapTile> CreateSurroundTiles(List<HexagonalMapTile> mapLayer)
        {
            var outerLayer = new List<HexagonalMapTile>();
            
            foreach (var tile in mapLayer)
            {
                var localOuterLayer = CreateSurroundTiles(tile);
                
                outerLayer.AddRange(localOuterLayer);
            }

            return outerLayer;
        }

        private List<HexagonalMapTile> CreateSurroundTiles(HexagonalMapTile mapTile)
        {
            var outerLayer = new List<HexagonalMapTile>();
            
            var centerVector = 2 * Vector3.forward;

            for (var i = 0; i < EdgeCount; i++)
            {
                if (mapTile.HasChainedTile(i))
                {
                    continue;
                }
                
                var orientation = mapTile.Orientation * Quaternion.AngleAxis(EdgeAngle * i, Vector3.up);
                var center = mapTile.Center + orientation * centerVector;
                var tile = CreateTile(center, orientation);
                
                outerLayer.Add(tile);
                
                tile.AddChainedTile(3, mapTile);
                mapTile.AddChainedTile(i, tile);
            
                var previousIndex = (EdgeCount + (i - 1)) % EdgeCount;
                var nextIndex = (EdgeCount + (i + 1)) % EdgeCount;
            
                if (mapTile.HasChainedTile(previousIndex))
                {
                    var previousIndexTile = mapTile.GetTileByIndex(previousIndex);

                    if (previousIndexTile != null)
                    {
                        previousIndexTile.AddChainedTile(2, tile);
                        tile.AddChainedTile(4, previousIndexTile);
                    }
                }
            
                if (mapTile.HasChainedTile(nextIndex))
                {
                    var nextIndexTile = mapTile.GetTileByIndex(nextIndex);

                    if (nextIndexTile != null)
                    {
                        nextIndexTile.AddChainedTile(4, tile);
                        tile.AddChainedTile(2, nextIndexTile);
                    }
                }
            }

            return outerLayer;
        }

        private HexagonalMapTile CreateTile(Vector3 center, Quaternion orientation)
        {
            var tile = new HexagonalMapTile();
            tile.SetCenter(center);
            tile.SetOrientation(orientation);

            var bounds = CreateBounds(center, orientation, 1);
            tile.SetBounds(bounds);

            return tile;
        }

        private List<Vector3> CreateBounds(Vector3 center, Quaternion orientation, float size)
        {
            var bounds = new List<Vector3>();
            var pointVector = size / Cos30Const * Vector3.forward;
            var pointOrientation = orientation * Quaternion.AngleAxis(EdgeAngle / 2f, Vector3.up);
            var rotateQuaternion = Quaternion.AngleAxis(EdgeAngle, Vector3.up);

            for (var i = 0; i < EdgeCount; i++)
            {
                bounds.Add(center + pointOrientation * pointVector);

                pointOrientation *= rotateQuaternion;
            }

            return bounds;
        }
    }
}