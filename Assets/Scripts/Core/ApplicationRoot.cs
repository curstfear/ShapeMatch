using UnityEngine;

namespace Core
{
    public class ApplicationRoot : MonoBehaviour
    {
        [SerializeField] private int _targetFps = 30;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Setup();
        }


        private void Setup()
        {
            Time.timeScale = 1f;
            Application.targetFrameRate = _targetFps;
        }
    }
}