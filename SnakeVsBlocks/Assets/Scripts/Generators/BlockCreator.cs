using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Block _blockPrefab;

    public Block Create(BlockSpawnPoint spawnPoint, int health)
    {
        var block = Instantiate(_blockPrefab, spawnPoint.transform.position, Quaternion.identity);
        block.Health = health;
        return block;
    }
}
