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

        public void SetCenter(Vector3 center)
        {
            Center = center;
        }
        
        public void AddChainedTile(int index, MapTile mapTile)
        {
            if (HasChainedTile(index))
            {
                return;
            }
            
            AddChainedTileInternal(index, mapTile);
        }
        
        public void AddChainedTile(MapTile mapTile)
        {
            AddChainedTileInternal(mapTile);
        }

        protected virtual void AddChainedTileInternal(MapTile mapTile)
        {
            AddChainedTileInternal(0, mapTile);
        }
        
        protected virtual void AddChainedTileInternal(int index, MapTile mapTile)
        {
            ChainedTiles.Add(index, mapTile);
        }
        
        protected virtual bool HasChainedTile(int index)
        {
            return ChainedTiles.ContainsKey(index);
        }
    }
}