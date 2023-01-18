using System.Collections.Generic;
using Common.Interfaces;
using UnityEngine;

namespace MapModule
{
    public interface IMapTile : IIdentified
    {
        Vector3 Center { get; }
        IEnumerable<Vector3> Bounds { get; }
        IEnumerable<IMapTile> ChainedTiles { get; }

        void SetCenter(Vector3 center);
        void SetBounds(List<Vector3> bounds);
        void AddChainedTile(IMapTile mapTile);
    }
}