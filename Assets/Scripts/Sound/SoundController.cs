using UnityEngine;
using Zenject;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tileClickSound;
    [SerializeField] private AudioClip _matchDestroySound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;


    private ActionBar _actionBar;

    [Inject]
    private void Construct(ActionBar actionBar)
    {
        _actionBar = actionBar;
    }


    private void OnEnable()
    {
        _actionBar.OnMatch.AddListener(PlayMatchDestroySound);
        _actionBar.OnWin.AddListener(PlayWinSound);
        _actionBar.OnLose.AddListener(PlayLoseSound);
        Tile.OnTileClicked.AddListener(PlayTileClickSound);
    }

    private void OnDisable()
    {
        _actionBar.OnMatch.RemoveListener(PlayMatchDestroySound);
        _actionBar.OnWin.RemoveListener(PlayWinSound);
        _actionBar.OnLose.RemoveListener(PlayLoseSound);
        Tile.OnTileClicked.RemoveListener(PlayTileClickSound);
    }


    private void PlayTileClickSound(Tile tile)
    {
        if (_audioSource != null && _tileClickSound != null)
            _audioSource.PlayOneShot(_tileClickSound);
    }

    private void PlayMatchDestroySound()
    {
        if (_audioSource != null && _matchDestroySound != null)
            _audioSource.PlayOneShot(_matchDestroySound);
    }

    private void PlayWinSound()
    {
        if (_audioSource != null && _winSound != null)
            _audioSource.PlayOneShot(_winSound);
    }

    private void PlayLoseSound()
    {
        if (_audioSource != null && _loseSound != null)
            _audioSource.PlayOneShot(_loseSound);
    }
}
