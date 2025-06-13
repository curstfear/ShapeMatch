using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CountTilesInArea => _tilesInArea.Count; 
    private List<Tile> _tilesInArea = new List<Tile>();

    public List<Tile> GetAllTilesInArea()
    {
        
        _tilesInArea = _tilesInArea.Where(tile => tile != null && tile.gameObject.activeInHierarchy).ToList();
        return new List<Tile>(_tilesInArea); 
    }

    public void AddTileToArea(Tile tile)
    {
        if (tile != null && !_tilesInArea.Contains(tile))
        {
            _tilesInArea.Add(tile);
        }
    }

    public void RemoveTileFromArea(Tile tile)
    {
        _tilesInArea.Remove(tile);
    }

    public void ClearAllTilesInArea()
    {
        _tilesInArea.Clear();
    }

    public void RefreshTilesList()
    {
        _tilesInArea = _tilesInArea.Where(tile => tile != null && tile.gameObject.activeInHierarchy).ToList();
    }
}