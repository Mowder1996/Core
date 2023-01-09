using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapModule
{
    public class HexagonalMapTile : MapTile
    {
        private const float Cos30Const = 0.866f;
        private const int EdgeAngle = 60;
        private const int EdgeCount = 6;
        
        public HexagonalMapTile(Vector3 center, Quaternion orientation, Dictionary<int, MapTile> chainedTiles)
            : this(center, orientation)
        {
            ChainedTiles = chainedTiles;
        }
        
        public HexagonalMapTile(Vector3 center, Quaternion orientation)
        {
            Id = Guid.NewGuid().ToString();
            ChainedTiles = new Dictionary<int, MapTile>();
            Center = center;
            Orientation = orientation;
            Bounds = CreateBounds(center, orientation, 1);
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