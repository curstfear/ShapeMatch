using UnityEngine;


public class ApplicationRoot : MonoBehaviour
{
    [SerializeField] private int _targetFps = 30;
    [SerializeField] private bool _isLockFPS = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Setup();
    }


    private void Setup()
    {
        Time.timeScale = 1f;
        if (_isLockFPS)
            Application.targetFrameRate = _targetFps;
    }
}
