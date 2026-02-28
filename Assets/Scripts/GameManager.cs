using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    private bool _gameHasEnded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void OnCompletion()
    {
        if (_gameHasEnded) return;

        _gameHasEnded = true;

        _restartButton.SetActive(true);       
        Debug.Log("Completion reached → Restart button is now visible");
    }

    public void RestartSimulator()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGame()  
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        Debug.Log("Game Quit requested!");
    }
}

