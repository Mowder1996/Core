using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapModule
{
    public class BaseMapTile : IMapTile
    {
        private readonly string _id = Guid.NewGuid().ToString();
        private readonly List<IMapTile> _chainedTiles = new List<IMapTile>();
        
        public string Id => _id;
        public Vector3 Center => _center;
        public IEnumerable<Vector3> Bounds => _bounds;
        public IEnumerable<IMapTile> ChainedTiles => _chainedTiles;

        private Vector3 _center;
        private List<Vector3> _bounds = new List<Vector3>();

        public void SetCenter(Vector3 center)
        {
            _center = center;
        }

        public void SetBounds(List<Vector3> bounds)
        {
            _bounds = bounds;
        }

        public void AddChainedTile(IMapTile mapTile)
        {
            if (HasChainedTile(mapTile))
            {
                return;
            }

            _chainedTiles.Add(mapTile);
        }

        public IMapTile GetTileById(string id)
        {
            return _chainedTiles.FirstOrDefault(item => item.Id.Equals(id));
        }

        public override bool Equals(object obj)
        {
            var tile = (IMapTile)obj;

            if (tile == null)
            {
                return false;
            }

            return _id.Equals(tile.Id);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(_id);
        }

        protected bool Equals(IMapTile other)
        {
            return _id.Equals(other.Id);
        }
        
        private bool HasChainedTile(IMapTile mapTile)
        {
            return _chainedTiles.Contains(mapTile);
        }
    }
}