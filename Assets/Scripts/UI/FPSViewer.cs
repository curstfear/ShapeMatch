using TMPro;
using UnityEngine;

public class FPSViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private float _deltaTime;

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _text.text = $"FPS: {Mathf.CeilToInt(fps)}";
    }
}
