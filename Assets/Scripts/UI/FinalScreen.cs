using DG.Tweening;
using UnityEngine;
using Zenject;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private ActionBar _actionBar;

    [Inject]
    private void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }

    private void Awake()
    {
        _winScreen.transform.localScale = Vector3.zero;
        _loseScreen.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        _actionBar.OnWin.AddListener(ShowWinScreen);
        _actionBar.OnLose.AddListener(ShowLoseScreen);
    }

    private void OnDisable()
    {
        _actionBar.OnWin.RemoveListener(ShowWinScreen);
        _actionBar.OnLose.RemoveListener(ShowLoseScreen);
    }

    private void ShowWinScreen()
    {
        _winScreen.transform.DOScale(1f, 0.3f).From(0f).SetEase(Ease.OutBounce);
    }

    private void ShowLoseScreen()
    {
        _loseScreen.transform.DOScale(1f, 0.3f).From(0f).SetEase(Ease.OutBounce);
    }
}
