using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenButtons : MonoBehaviour
{
    [SerializeField] private int _gameSceneNumber;
    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneNumber);
    }
}
