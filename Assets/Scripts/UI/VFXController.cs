using UnityEngine;
using Zenject;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject _matchDestroyVFX;
    [SerializeField] private Transform[] _matchDestroyTransformPoints;
    
    private ActionBar _actionBar;

    [Inject]
    private void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }

    private void OnEnable()
    {
        _actionBar.OnMatch.AddListener(MatchDestroyPlayVFX);
    }

    private void OnDisable()
    {
        _actionBar.OnMatch.RemoveListener(MatchDestroyPlayVFX);
    }


    public void MatchDestroyPlayVFX()
    {
        for (int i = 0; i < _matchDestroyTransformPoints.Length; i++)
        {
            Instantiate(_matchDestroyVFX, _matchDestroyTransformPoints[i]);
        }
    }
}
