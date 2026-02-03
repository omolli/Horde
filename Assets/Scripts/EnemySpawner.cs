using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] float _spawnCooldown;
    [SerializeField] float _difficultyMultiplier;
    float _currentCooldown;
    [SerializeField] Tilemap _groundTiles;
    List<Vector3> _spawnPositions = new();

    private void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(IncreaseDifficulty), 3f, 3f);
    }

    private void Update()
    {
        HandleEnemySpawn();
    }

    void HandleEnemySpawn()
    {
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown > Time.time)
        {
            return;
        }
        _currentCooldown = Time.time + _spawnCooldown;
        SpawnEnemyToRndLocation();
    }

    void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }

    void SpawnEnemyToRndLocation()
    {
        Instantiate(_enemyPrefab, GetRndLocation(),Quaternion.identity);
    }

    Vector3 GetRndLocation()
    {
        return _spawnPositions[Random.Range(0,_spawnPositions.Count)];
    }

    void IncreaseDifficulty()
    {
        _spawnCooldown *= 1 - _difficultyMultiplier / 100;
    }
}
