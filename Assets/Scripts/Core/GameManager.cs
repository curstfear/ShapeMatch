using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CountTilesInArea = 0;

    public bool AreTilesClickableInArea { get; private set; } = false;

    public void SetTilesClickableInArea(bool clickable)
    {
        if (AreTilesClickableInArea != clickable)
        {
            AreTilesClickableInArea = clickable;

        }
    }
}
