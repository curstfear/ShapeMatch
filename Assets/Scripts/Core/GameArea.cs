using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameArea : MonoBehaviour
{
    private GameManager _gameManager;
    private Canvas _canvas;
    private TileSpawner _tileSpawner;
    public static UnityEvent<bool> OnTilesClickabilityChanged = new UnityEvent<bool>();

    [Inject]
    private void Construct(GameManager gameManager, Canvas canvas, TileSpawner tileSpawner)
    {
        _gameManager = gameManager;
        _canvas = canvas;
        _tileSpawner = tileSpawner;
    }

    private void Start()
    {
        CheckAndSetTilesClickability();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            _gameManager.CountTilesInArea++;
            CheckCanvasOrder();
            CheckAndSetTilesClickability();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            _gameManager.CountTilesInArea--;
            CheckCanvasOrder();
        }
    }

    private void CheckAndSetTilesClickability()
    {
        int totalTilesToSpawn = _tileSpawner.GetTotalTilesToSpawn();
        bool shouldBeClickable = (_gameManager.CountTilesInArea == totalTilesToSpawn);

        if (shouldBeClickable != _gameManager.AreTilesClickableInArea)
        {
            _gameManager.SetTilesClickableInArea(shouldBeClickable);
            OnTilesClickabilityChanged.Invoke(shouldBeClickable);
        }
    }

    private void CheckCanvasOrder()
    {
        int totalTiles = _tileSpawner.GetTotalTilesToSpawn();
        if (_gameManager.CountTilesInArea == totalTiles && _canvas != null)
        {
            _canvas.sortingOrder = -1;
        }
        else if (_gameManager.CountTilesInArea == 0 && _canvas != null)
        {
            _canvas.sortingOrder = 0;
        }
    }
}