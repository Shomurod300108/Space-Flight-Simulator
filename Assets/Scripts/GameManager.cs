using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        // Press Escape to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            // Stop play mode in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the build
            Application.Quit();
        #endif

        Debug.Log("Game Quit!");
    }
}

