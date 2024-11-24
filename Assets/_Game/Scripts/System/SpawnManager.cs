using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPrefabs;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        foreach (var spawnPrefab in spawnPrefabs)
        {
            var spawnObject = Instantiate(spawnPrefab);
            InjectCamera(spawnObject);
        }
    }

    void InjectCamera(GameObject spawnObject)
    {
        if (spawnObject.TryGetComponent(out Canvas canvas))
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
        }
    }
}
