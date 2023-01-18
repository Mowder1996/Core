// using UnityEngine;
//
// namespace MapModule
// {
//     public class TestMapFactory : HexagonalCircleMapFactory
//     {
//         public override IMapModel Create()
//         {
//             var hexagonalCircleMap = base.Create();
//
//             for (var i = 0; i < EdgeCount; i++)
//             {
//                 var cornerTile = GetCornerTile(hexagonalCircleMap, i, 3);
//                 var targetTile = cornerTile;                
//                 
//                 for (var j = 0; j < 1; j++)
//                 {
//                     targetTile = targetTile.ChainedTiles[2];
//                 }
//
//                 var center = targetTile.Center + targetTile.Orientation * Vector3.forward * 2;
//                 var tile = CreateTile(center, targetTile.Orientation);
//             }
//             
//             return hexagonalCircleMap;
//         }
//
//         private MapTile GetCornerTile(IMapModel mapModel, int index, int layerCount)
//         {
//             var rootTile = mapModel.GetTileById(string.Empty);
//             var targetTile = rootTile;
//
//             for (var i = 0; i < layerCount; i++)
//             {
//                 targetTile = targetTile.ChainedTiles[0];
//             }
//
//             return targetTile;
//         }
//     }
// }