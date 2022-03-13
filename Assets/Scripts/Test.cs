using System;
using ContentLoader.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private PrefabSpawnerService _prefabSpawnerService;
    
    [Inject]
    private void Construct(PrefabSpawnerService prefabSpawnerService)
    {
        _prefabSpawnerService = prefabSpawnerService;
    }

    private async void Start()
    {
        var handler1 = await _prefabSpawnerService.SpawnPrefabFromFactory("Cube");

        Debug.Log($"GO name: {handler1.Instance.name}");

        var handler2 = await _prefabSpawnerService.SpawnPrefabFromFactory<MeshFilter>("Cube");

        Debug.Log($"Vertices count: {handler2.Instance.mesh.vertices.Length}");

        await UniTask.Delay(TimeSpan.FromSeconds(3));
            
        handler1.Unload();
        handler2.Unload();
            
        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }
}
