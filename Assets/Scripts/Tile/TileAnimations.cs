using DG.Tweening;
using UnityEngine;
using Zenject;

public class TileAnimations : MonoBehaviour
{
    private Tile _tile;
    private ActionBar _actionBar;

    [Inject]
    private void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }

    private void Awake()
    {
        _tile = GetComponent<Tile>();
    }

    public void PlayMoveToActionBarAnimation()
    {
        Transform originalParent = transform.parent;

        transform.DOScale(0f, 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                _actionBar.AddTile(_tile);
                Transform slot = _actionBar.GetNextSlotTransform();
                if (slot != null)
                {
                    transform.SetParent(slot, false);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    transform.localScale = Vector3.zero;
                    transform.DOScale(Vector3.one * 35f, 0.3f)
                        .SetEase(Ease.OutBounce);
                }
                else
                {
                    transform.SetParent(originalParent, false);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    transform.DOScale(Vector3.one, 0.2f)
                        .SetEase(Ease.OutBounce)
                        .OnComplete(() => _tile.SetClickable(true));
                }
            });
    }
}