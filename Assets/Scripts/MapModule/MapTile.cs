using System.Collections.Generic;
using Common.Interfaces;
using UnityEngine;

namespace MapModule
{
    public class MapTile : IIdentified
    {
        public Dictionary<int, MapTile> ChainedTiles { get; protected set; }
        public Vector3 Center { get; protected set; }
        public Quaternion Orientation { get; protected set; }
        public List<Vector3> Bounds { get; protected set; }
        public string Id { get; protected set; }

        public virtual void AddChainedTile(int index, MapTile mapTile)
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