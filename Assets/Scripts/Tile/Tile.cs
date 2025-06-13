using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shapeSprite;
    [SerializeField] private SpriteRenderer _animalIconSprite;
    [SerializeField] private SpriteRenderer _borderSprite;

    private ShapeData _data;
    private bool _isClickable = false;
    private TileAnimations _tileAnimations;
    private ActionBar _actionBar;

    [Inject]
    private void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }

    private void Awake()
    {
        _tileAnimations = GetComponent<TileAnimations>();
    }

    private void Start()
    {
        GameArea.OnTilesClickabilityChanged.AddListener(SetClickable);
    }

    private void OnDestroy()
    {
        GameArea.OnTilesClickabilityChanged.RemoveListener(SetClickable);
    }

    public void Initialize(ShapeData data, ShapeVisualConfig visualConfig)
    {
        _data = data;
        Color color = data.ShapeColor;
        color.a = 1f;

        _shapeSprite.color = color;
        _animalIconSprite.sprite = data.AnimalIcon;
        _shapeSprite.sprite = visualConfig.GetShapeSprite(data.Shape);
        _borderSprite.sprite = visualConfig.GetShapeBorderSprite(data.Shape);

        SetupCollider(visualConfig);
        gameObject.tag = "Tile";
    }

    private void SetupCollider(ShapeVisualConfig visualConfig)
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            if (Application.isPlaying)
                Destroy(collider);
            else
                DestroyImmediate(collider);
        }

        Collider2D sourceCollider = visualConfig.GetShapeCollider(_data.Shape);
        Collider2D newCollider = null;

        if (sourceCollider != null)
        {
            if (sourceCollider is CircleCollider2D circle)
            {
                CircleCollider2D collider2D = gameObject.AddComponent<CircleCollider2D>();
                collider2D.radius = circle.radius;
                collider2D.isTrigger = false;
                newCollider = collider2D;
            }
            else if (sourceCollider is PolygonCollider2D polygon)
            {
                PolygonCollider2D collider2D = gameObject.AddComponent<PolygonCollider2D>();
                collider2D.pathCount = polygon.pathCount;
                for (int i = 0; i < polygon.pathCount; i++)
                {
                    Vector2[] path = new Vector2[polygon.GetPath(i).Length];
                    polygon.GetPath(i).CopyTo(path, 0);
                    collider2D.SetPath(i, path);
                }
                collider2D.isTrigger = false;
                newCollider = collider2D;
            }
            else if (sourceCollider is BoxCollider2D box)
            {
                BoxCollider2D collider2D = gameObject.AddComponent<BoxCollider2D>();
                collider2D.size = box.size;
                collider2D.offset = box.offset;
                collider2D.isTrigger = false;
                newCollider = collider2D;
            }
        }

        if (newCollider == null)
        {
            CircleCollider2D defaultCollider = gameObject.AddComponent<CircleCollider2D>();
            defaultCollider.radius = 0.5f;
            defaultCollider.isTrigger = false;
        }
    }

    private void OnMouseDown()
    {
        if (_isClickable && _tileAnimations != null)
        {
            _isClickable = false; // Чтобы не кликали несколько раз
            _tileAnimations.PlayMoveToActionBarAnimation();
        }
    }

    public void OnTouch()
    {
        if (_isClickable && _tileAnimations != null)
        {
            _isClickable = false; // Чтобы не кликали несколько раз
            _tileAnimations.PlayMoveToActionBarAnimation();
        }
    }

    public void SetClickable(bool clickable)
    {
        _isClickable = clickable;
    }

    public ShapeData GetData() => _data;
}