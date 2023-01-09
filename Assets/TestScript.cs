using System.Collections.Generic;
using System.Linq;
using MapModule;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private bool _isMapCreated;
    private MapTile _rootTile;
    private List<string> _createdIds = new List<string>();

    private void Start()
    {
        var factory = new HexagonalCircleMapFactory();
        _rootTile = factory.Create().GetTileById(string.Empty);
        _isMapCreated = true;
    }

    public void OnGUI()
    {
        if (!_isMapCreated)
        {
            return;
        }

        DrawTile(_rootTile);
        
        for (var i = 0; i < 6; i++)
        {
            if (!_rootTile.ChainedTiles.ContainsKey(i))
            {
                continue;
            }
            
            for (var j = 0; j < 6; j++)
            {
                if (!_rootTile.ChainedTiles[i].ChainedTiles.ContainsKey(j))
                {
                    continue;
                }
            
                Debug.DrawLine(_rootTile.ChainedTiles[i].Center, _rootTile.ChainedTiles[i].ChainedTiles[j].Center, Color.red, 10);
            }
        }
    }

    private void DrawTile(MapTile mapTile)
    {
        if (_createdIds.Contains(mapTile.Id))
        {
            return;
        }
        
        var bounds = mapTile.Bounds;

        for (var i = 0; i < bounds.Count; i++)
        {
            Debug.DrawLine(bounds[i], bounds[(i + 1) % bounds.Count], Color.magenta, 10);
        }

        // for (var i = 0; i < 6; i++)
        // {
        //     if (!mapTile.ChainedTiles.ContainsKey(i))
        //     {
        //         continue;
        //     }
        //     
        //     Debug.DrawLine(mapTile.Center, mapTile.ChainedTiles[i].Center, Color.red, 10);
        // }

        _createdIds.Add(mapTile.Id);
        
        if (!mapTile.ChainedTiles.Any())
        {
            return;
        }
        
        foreach (var tile in mapTile.ChainedTiles)
        {
            DrawTile(tile.Value);
        }
    }
}
