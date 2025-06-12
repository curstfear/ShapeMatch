using UnityEngine;

[CreateAssetMenu(fileName = "NewShapeVisual", menuName = "ShapeMatch/ShapeVisuals")]
public class ShapeVisualConfig : ScriptableObject
{
    [Header("Predefined Sprites")]
    public Sprite circleSprite;
    public Sprite squareSprite;
    public Sprite triangleSprite;

    [Header("Predefined Border Sprites")]
    public Sprite circleBorder;
    public Sprite squareBorder;
    public Sprite triangleBorder;

    [Header("Predefined Colliders")]
    public CircleCollider2D circleCollider;
    public BoxCollider2D squareCollider;
    public PolygonCollider2D triangleCollider;




    public Sprite GetShapeSprite(Shape shape)
    {
        return shape switch
        {
            Shape.Circle => circleSprite,
            Shape.Square => squareSprite,
            Shape.Triangle => triangleSprite,
            _ => null
        };
    }

    public Sprite GetShapeBorderSprite(Shape shape)
    {
        return shape switch
        {
            Shape.Circle => circleBorder,
            Shape.Square => squareBorder,
            Shape.Triangle => triangleBorder,
            _ => null
        };
    }



    public Collider2D GetShapeCollider(Shape shape)
    {
        return shape switch
        {
            Shape.Circle => circleCollider,
            Shape.Square => squareCollider,
            Shape.Triangle => triangleCollider,
            _ => null
        };
    }
}