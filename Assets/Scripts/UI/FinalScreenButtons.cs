using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenButtons : MonoBehaviour
{
    [SerializeField] private int _menuSceneNumber;
    [SerializeField] private int _gameSceneNumber;
    public void ExitButton()
    {
        SceneManager.LoadScene(_menuSceneNumber);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(_gameSceneNumber);
    }
}
