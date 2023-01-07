using System;
using System.Collections.Generic;
using Common.Interfaces;
using UnityEngine;

namespace MapModule
{
    public struct MapTile : IIdentified
    {
        public Dictionary<int, MapTile> ChainedTiles;
        public Vector3 Center;
        public List<Vector3> Bounds;

        public string Id => Guid.NewGuid().ToString();

        public MapTile(Vector3 center, List<Vector3> bounds, Dictionary<int, MapTile> chainedTiles) : this(center, bounds)
        {
            ChainedTiles = chainedTiles;
        }
        
        public MapTile(Vector3 center, List<Vector3> bounds)
        {
            ChainedTiles = new Dictionary<int, MapTile>();
            Center = center;
            Bounds = bounds;
        }

        public void AddChainedTile(int index, MapTile mapTile)
        {
            if (ChainedTiles.ContainsKey(index))
            {
                ChainedTiles[index] = mapTile;
                
                return;
            }
            
            ChainedTiles.Add(index, mapTile);
        }
    }
}