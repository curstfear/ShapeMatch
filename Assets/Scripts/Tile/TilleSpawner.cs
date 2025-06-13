using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TileSpawner : MonoBehaviour
{
    public UnityEvent OnSpawnStarted = new UnityEvent();
    public UnityEvent OnSpawnCompleted = new UnityEvent();

    [SerializeField] private float _spawnInterval = 0.1f;
    [SerializeField] private int _shapeMultiplier = 3;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private ShapeVisualConfig _shapeVisuals;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private List<ShapeData> _allTilesData;

    private DiContainer _container;
    private List<Tile> _spawnedTiles = new List<Tile>();
    private int _currentTotalTiles;
    private RefreshButton _refreshButton;

    [Inject]
    private void Construct(DiContainer container, RefreshButton refreshButton)
    {
        _container = container;
        _refreshButton = refreshButton;
    }

    private void Start()
    {
        StartCoroutine(SpawnTiles());
    }

    private IEnumerator SpawnTiles()
    {
        _currentTotalTiles = _allTilesData.Count * _shapeMultiplier;
        OnSpawnStarted?.Invoke();

        if (_refreshButton != null)
            _refreshButton.SetActionBarSortingOrder(10);

        foreach (ShapeData data in _allTilesData)
        {
            for (int i = 0; i < _shapeMultiplier; i++)
            {
                Vector3 spawnPos = transform.position;
                GameObject tileGO = _container.InstantiatePrefab(_tilePrefab, spawnPos, Quaternion.identity, _spawnArea);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.Initialize(data, _shapeVisuals);
                _spawnedTiles.Add(tile);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        OnSpawnCompleted?.Invoke();

        yield return new WaitForSeconds(0.5f);

        if (_refreshButton != null)
            _refreshButton.OnAllTilesSpawned();
    }

    public void ClearTiles()
    {
        foreach (Tile tile in _spawnedTiles)
        {
            if (tile != null)
                Destroy(tile.gameObject);
        }
        _spawnedTiles.Clear();
        _currentTotalTiles = 0;
    }

    public void SpawnRandomTiles(int count)
    {
        if (count % 3 != 0)
        {
            count = Mathf.CeilToInt((float)count / 3) * 3;
        }

        StartCoroutine(SpawnRandomTilesCoroutine(count));
    }

    private IEnumerator SpawnRandomTilesCoroutine(int count)
    {
        _currentTotalTiles = count;
        int groupsCount = count / _shapeMultiplier;

        OnSpawnStarted?.Invoke();

        
        if (_refreshButton != null)
            _refreshButton.SetActionBarSortingOrder(10);
        
        List<ShapeData> availableShapes = new List<ShapeData>(_allTilesData);

        
        for (int groupIndex = 0; groupIndex < groupsCount; groupIndex++)
        {
            
            if (availableShapes.Count == 0)
            {
                availableShapes = new List<ShapeData>(_allTilesData);
            }

            
            int randomIndex = Random.Range(0, availableShapes.Count);
            ShapeData selectedShape = availableShapes[randomIndex];
            availableShapes.RemoveAt(randomIndex);

            // Спавним 3 одинаковые фигурки
            for (int tileInGroup = 0; tileInGroup < _shapeMultiplier; tileInGroup++)
            {
                Vector3 spawnPos = transform.position;
                GameObject tileGO = _container.InstantiatePrefab(_tilePrefab, spawnPos, Quaternion.identity, _spawnArea);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.Initialize(selectedShape, _shapeVisuals);
                _spawnedTiles.Add(tile);

                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        OnSpawnCompleted?.Invoke();

        yield return new WaitForSeconds(0.5f);

        if (_refreshButton != null)
            _refreshButton.OnAllTilesSpawned();
    }

    public int GetTotalTilesToSpawn() => _currentTotalTiles;
    public int GetCurrentTileCount() => _spawnedTiles.Count;
}