using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string gameSceneName = "Exercise3_Asteroids";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void BackToMainMenu()
    {
        // Resets the time scale in case the game was paused 
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        // Resets the time scale in case the game was paused 
        Time.timeScale = 1f;

        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Logs a message in the console to confirm it works in the editor
        Debug.Log("Game is exiting...");

        // Quits the actual application build
        Application.Quit();

        // Quits the play mode in the editor 
        UnityEditor.EditorApplication.ExitPlaymode();
    }
}
