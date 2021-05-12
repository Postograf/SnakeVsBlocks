using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCreator : MonoBehaviour
{
    [SerializeField] private Bonus _bonusPrefab;
    [SerializeField] private Vector2Int _bonusSizeRange;

    public Bonus Create(BlockSpawnPoint spawnPoint)
    {
        var bonus = Instantiate(_bonusPrefab, spawnPoint.transform.position, Quaternion.identity);
        bonus.BonusSize = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);
        return bonus;
    }
}
