using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RefreshButton : MonoBehaviour
{
    [SerializeField] private int _refreshCount = 5;
    [SerializeField] private TMP_Text _refreshCountText;

    private GameManager _gameManager;
    private TileSpawner _tileSpawner;
    private ActionBar _actionBar;
    private Canvas _actionBarCanvas;
    private bool _isRestarting = false;

    private int RefreshCount
    {
        get
        {
            return _refreshCount;
        }
        set
        {
            _refreshCount = value;
            _refreshCountText.text = _refreshCount.ToString();
        }
    }

    [Inject]
    private void Construct(GameManager gameManager, TileSpawner tileSpawner, ActionBar actionBar)
    {
        _gameManager = gameManager;
        _tileSpawner = tileSpawner;
        _actionBar = actionBar;

        _actionBarCanvas = _actionBar.GetComponent<Canvas>();
        if (_actionBarCanvas == null)
            _actionBarCanvas = _actionBar.GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        _tileSpawner.OnSpawnStarted.AddListener(() => _isRestarting = true);
        _tileSpawner.OnSpawnCompleted.AddListener(() => _isRestarting = false);
    }

    private void OnDisable()
    {
        _tileSpawner.OnSpawnStarted.RemoveListener(() => _isRestarting = true);
        _tileSpawner.OnSpawnCompleted.RemoveListener(() => _isRestarting = false);
    }

    public void Refresh()
    {
        if (_isRestarting || RefreshCount <= 0)
            return;

        _isRestarting = true;


        int tilesInArea = _gameManager.CountTilesInArea;
        int tilesInActionBar = _actionBar.GetTileCount();
        int totalTiles = tilesInArea + tilesInActionBar;


        if (totalTiles == 0)
            totalTiles = 12;


        int adjustedCount = Mathf.CeilToInt((float)totalTiles / 3) * 3;


        _tileSpawner.ClearTiles();
        _actionBar.ClearBar();
        _gameManager.ClearAllTilesInArea();


        _tileSpawner.SpawnRandomTiles(adjustedCount);
        --RefreshCount;
    }

    public void OnAllTilesSpawned()
    {
        if (_actionBarCanvas != null)
        {
            _actionBarCanvas.sortingOrder = -1;
        }
    }

    public void SetActionBarSortingOrder(int order)
    {
        if (_actionBarCanvas != null)
        {
            _actionBarCanvas.sortingOrder = order;
        }
    }
}