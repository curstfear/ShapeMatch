using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 0.1f;
    [SerializeField] private int _shapeMultiplier = 3;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private ShapeVisualConfig _shapeVisuals;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private List<ShapeData> _allTilesData;

    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        foreach (ShapeData data in _allTilesData)
        {
            for (int i = 0; i < _shapeMultiplier; i++)
            {
                Vector3 spawnPos = transform.position;

                // Создаем тайл через Zenject для инжекции ActionBar
                GameObject tileGO = _container.InstantiatePrefab(_tilePrefab, spawnPos, Quaternion.identity, _spawnArea);
                Tile tile = tileGO.GetComponent<Tile>();

                // Инициализируем
                tile.Initialize(data, _shapeVisuals);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    public int GetTotalTilesToSpawn() => _allTilesData.Count * _shapeMultiplier;
}