using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private ShapeVisualConfig _shapeVisuals;
    [SerializeField] private List<ShapeData> _allTilesData;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval;
 
    private void Start()
    {
        StartCoroutine(SpawnTiles());
    }

    private System.Collections.IEnumerator SpawnTiles()
    {
        foreach (ShapeData data in _allTilesData)
        {
            for (int i = 0; i < 3; i++) // Создаём 3 копии каждого конфига
            {
                Vector3 spawnPos = transform.position;
                GameObject tileGO = Instantiate(_tilePrefab, spawnPos, Quaternion.identity, _spawnArea);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.Initialize(data, _shapeVisuals);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    private List<ShapeData> GenerateTileSet()
    {
        List<ShapeData> tiles = new List<ShapeData>();
        int tilesPerType = 3; // Кратность трём
        int totalTiles = 30; // Примерное количество фигурок на поле
        int typesToUse = totalTiles / tilesPerType;

        for (int i = 0; i < typesToUse; i++)
        {
            ShapeData randomTile = _allTilesData[Random.Range(0, _allTilesData.Count)];
            for (int j = 0; j < tilesPerType; j++)
            {
                tiles.Add(randomTile);
            }
        }
        return tiles;
    }
}
