using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Zenject;

public class ActionBar : MonoBehaviour
{
    public UnityEvent OnLose;
    public UnityEvent OnWin;

    public UnityEvent OnMatch;

    [SerializeField] private Transform[] _slots = new Transform[7];

    private List<Tile> _tiles = new List<Tile>();
    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void AddTile(Tile tile)
    {
        if (_tiles.Count >= _slots.Length)
        {
            OnLose.Invoke();
            return;
        }

        _tiles.Add(tile);
        tile.SetClickable(false);

        Collider2D collider = tile.GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        Rigidbody2D rb = tile.GetComponent<Rigidbody2D>();
        if (rb != null)
            Destroy(rb);

        CheckAndRemoveAnyMatches();
    }

    private void CheckAndRemoveAnyMatches()
    {
        Dictionary<ShapeData, List<Tile>> groupedTiles = new Dictionary<ShapeData, List<Tile>>();

        foreach (Tile tile in _tiles)
        {
            if (tile == null) continue;

            ShapeData data = tile.GetData();
            if (!groupedTiles.ContainsKey(data))
            {
                groupedTiles[data] = new List<Tile>();
            }
            groupedTiles[data].Add(tile);
        }

        List<Tile> tilesToRemove = new List<Tile>();
        bool matchFound = false;

        foreach (var entry in groupedTiles)
        {
            if (entry.Value.Count >= 3)
            {
                tilesToRemove.AddRange(entry.Value);
                matchFound = true;
                OnMatch?.Invoke();
            }
        }

        if (matchFound)
        {
            foreach (Tile tile in tilesToRemove)
            {
                if (tile != null)
                {
                    Destroy(tile.gameObject);
                }
            }

            _tiles = _tiles.Except(tilesToRemove).ToList();

            ReorganizeTiles();

            CheckAndRemoveAnyMatches();
        }
        else
        {
            CheckGameState();
        }
    }

    private void ReorganizeTiles()
    {

        for (int i = 0; i < _tiles.Count; i++)
        {
            if (_tiles[i] != null && i < _slots.Length && _slots[i] != null)
            {
                _tiles[i].transform.SetParent(_slots[i], false);
                _tiles[i].transform.localPosition = Vector3.zero;
                _tiles[i].transform.localRotation = Quaternion.identity;
            }
        }

        for (int i = _tiles.Count; i < _slots.Length; i++)
        {
            if (_slots[i] != null)
            {
                while (_slots[i].childCount > 0)
                {
                    DestroyImmediate(_slots[i].GetChild(0).gameObject);
                }
            }
        }
    }

    private void CheckGameState()
    {
        if (_tiles.Count >= _slots.Length)
        {
            OnLose.Invoke();
        }
        else if (_gameManager != null && _gameManager.CountTilesInArea == 0 && _tiles.Count == 0)
        {
            OnWin.Invoke();
        }
    }

    public void ClearBar()
    {
        foreach (Tile tile in _tiles.ToArray())
        {
            if (tile != null)
                Destroy(tile.gameObject);
        }
        _tiles.Clear();
    }

    public Transform GetNextSlotTransform()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].childCount == 0)
            {
                return _slots[i];
            }
        }
        return null;
    }
    public int GetTileCount() => _tiles.Count;

    public List<Tile> GetAllTiles()
    {
        return _tiles.Where(tile => tile != null).ToList();
    }
}