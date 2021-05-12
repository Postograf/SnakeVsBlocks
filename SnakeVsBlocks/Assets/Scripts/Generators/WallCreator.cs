using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField] private Wall _wallPrefab;

    public Wall Create(WallSpawnPoint spawnPoint)
    {
        var wall = Instantiate(_wallPrefab, spawnPoint.transform.position, Quaternion.identity);
        return wall;
    }
    
    public Wall Create(Vector2 spawnPoint, float scaleMultiplierY = 1)
    {
        var wall = Instantiate(_wallPrefab, spawnPoint, Quaternion.identity);

        var scale = wall.transform.localScale;
        scale.y *= scaleMultiplierY;
        wall.transform.localScale = scale;

        return wall;
    }
}
