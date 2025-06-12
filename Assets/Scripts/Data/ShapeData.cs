using UnityEngine;

[CreateAssetMenu(fileName = "NewTileConfig", menuName = "ShapeMatch/TileData")]
public class ShapeData : ScriptableObject 
{
    public Sprite AnimalIcon;
    public Color ShapeColor;
    public Shape Shape;
    public SpecialType SpecialType;
}
