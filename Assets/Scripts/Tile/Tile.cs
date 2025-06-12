using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shapeSprite;
    [SerializeField] private SpriteRenderer _animalIconSprite;
    [SerializeField] private SpriteRenderer _borderSprite;

    private ShapeData _data;

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
    }

    private void SetupCollider(ShapeVisualConfig visualConfig)
    {
        // Удаляем старые коллайдеры
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            Destroy(collider);
        }

        Collider2D sourceCollider = visualConfig.GetShapeCollider(_data.Shape);
        if (sourceCollider != null)
        {
            if (sourceCollider is CircleCollider2D circle)
            {
                CircleCollider2D collider2D = gameObject.AddComponent<CircleCollider2D>();
                collider2D.radius = circle.radius; // Копируем радиус
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
            }
        }
        else
        {
            Debug.LogWarning("No collider defined for shape: " + _data.Shape);
        }
    }

    public ShapeData GetData() => _data;
}