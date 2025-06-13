using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameArea : MonoBehaviour
{
    public UnityEvent<bool> OnAllTilesInArea = new UnityEvent<bool>();

    private GameManager _gameManager;
    private Canvas _canvas;
    private TileSpawner _tileSpawner;

    [Inject]
    private void Construct(GameManager gameManager, Canvas canvas, TileSpawner tileSpawner)
    {
        _gameManager = gameManager;
        _canvas = canvas;
        _tileSpawner = tileSpawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            Tile tile = collision.GetComponent<Tile>();
            if (tile != null)
            {
                _gameManager.AddTileToArea(tile);
                CheckAllTilesInArea();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            Tile tile = collision.GetComponent<Tile>();
            if (tile != null)
            {
                _gameManager.RemoveTileFromArea(tile);
                CheckAllTilesInArea();
            }
        }
    }

    private void CheckAllTilesInArea()
    {
        int totalTiles = _tileSpawner.GetTotalTilesToSpawn();
        int tilesInArea = _gameManager.CountTilesInArea;

        if (tilesInArea == totalTiles && _canvas != null)
        {
            _canvas.sortingOrder = -1;
            OnAllTilesInArea?.Invoke(true);
        }
        else if (tilesInArea == 0 && _canvas != null)
        {
            _canvas.sortingOrder = 0;
            OnAllTilesInArea?.Invoke(false);
        }
    }
}