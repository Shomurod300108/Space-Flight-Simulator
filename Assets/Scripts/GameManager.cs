using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;          

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private PlayableDirector _finalTimeline;  

    private bool _gameHasEnded = false;

    void Start()
    {
        if (_restartButton != null)
            _restartButton.SetActive(false);

        if (_finalTimeline != null)
        {
            _finalTimeline.stopped += OnFinalTimelineStopped;
        }
        else
        {
            Debug.LogWarning("Final Timeline not assigned in GameManager!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void OnFinalTimelineStopped(PlayableDirector director)
    {
        if (_gameHasEnded) return;

        _gameHasEnded = true;

        if (_restartButton != null)
        {
            _restartButton.SetActive(true);
        }

    }

    public void RestartSimulator()
    {
        _gameHasEnded = false;
        if (_restartButton != null)
            _restartButton.SetActive(false);

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

    void OnDestroy()
    {
        if (_finalTimeline != null)
        {
            _finalTimeline.stopped -= OnFinalTimelineStopped;
        }
    }
}

