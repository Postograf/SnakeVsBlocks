using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] private int _fullLinesCount;
    [SerializeField] private int _randomLinesBetweenFullLines;
    [SerializeField] private int _snakeLength;
    [SerializeField] private float _step;

    [Header("Blocks")]
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private int _blockChance;

    [Header("Walls")]
    [SerializeField] private WallCreator _wallCreator;
    [SerializeField] private float _limitersAdditionalHeight;
    [SerializeField] private int _wallChance;

    [Header("Bonuses")]
    [SerializeField] private BonusCreator _bonusCreator;
    [SerializeField] private int _bonusChance;

    private BlockSpawnPoint[] _blockSpawnPoints;
    private WallSpawnPoint[] _wallSpawnPoints;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private void Start()
    {
        _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        var startPosition = transform.position;

        for (; _fullLinesCount > 0; _fullLinesCount--)
        {
            for(int i = 0; i < _randomLinesBetweenFullLines; i++)
            {
                GenerateRandomLine();
                GenerateWalls();
                Move();
            }
                
            GenerateFullLine();
            GenerateWalls();
            Move();
        }

        var endPosition = transform.position;

        CreateLimiters(startPosition, endPosition);
    }

    private void GenerateRandomLine()
    {
        var maximumLengthBoost = 0;

        foreach (var spawnPoint in _blockSpawnPoints)
        {
            var blockCount = 0;

            if (
                Random.Range(0, 100) < _blockChance 
                && ++blockCount < _blockSpawnPoints.Length
            )
            {
                var randomHealth = Random.Range(1, _snakeLength);
                _blockCreator.Create(spawnPoint, randomHealth);
            }
            else if(Random.Range(0, 100) < _bonusChance)
            {
                var bonus = _bonusCreator.Create(spawnPoint);

                if (bonus.BonusSize > maximumLengthBoost)
                    maximumLengthBoost = bonus.BonusSize;
            }
        }
        _snakeLength += maximumLengthBoost;
    }

    private void GenerateFullLine()
    {
        var minimumLengthReduction = int.MaxValue;

        foreach (var spawnPoint in _blockSpawnPoints) 
        {
            var randomHealth = Random.Range(1, _snakeLength);
            _blockCreator.Create(spawnPoint, randomHealth);

            if (randomHealth < minimumLengthReduction)
                minimumLengthReduction = randomHealth;
        }

        _snakeLength -= minimumLengthReduction;
    }

    private void GenerateWalls()
    {
        foreach (var spawnPoint in _wallSpawnPoints)
        {
            if (Random.Range(0, 100) < _wallChance)
                _wallCreator.Create(spawnPoint);
        }
    }

    private void CreateLimiters(Vector2 start, Vector2 end)
    {
        var leftX = _blockSpawnPoints.Min(x => x.transform.position.x);
        leftX -= Block.Scale.x / 2 - Wall.Scale.x / 2;

        var rightX = _blockSpawnPoints.Max(x => x.transform.position.x);
        rightX += Block.Scale.x / 2 + Wall.Scale.x / 2;

        var scaleY = end.y - start.y + _limitersAdditionalHeight;
        var positionY = (end.y + start.y) / 2;

        var leftPosition = new Vector2(leftX, positionY);
        var rightPosition = new Vector2(rightX, positionY);

        _wallCreator.Create(leftPosition, scaleY);
        _wallCreator.Create(rightPosition, scaleY);
    }

    private void Move()
    {
        var nextPosition = transform.position;
        nextPosition.y += _step;
        transform.position = nextPosition;
    }
}
