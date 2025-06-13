using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject _matchDestroyVFX;
    [SerializeField] private Transform[] _matchDestroyTransformPoints;

    public void MatchDestroyPlayVFX()
    {
        for (int i = 0; i < _matchDestroyTransformPoints.Length; i++)
        {
            Instantiate(_matchDestroyVFX, _matchDestroyTransformPoints[i]);
        }
    }
}
