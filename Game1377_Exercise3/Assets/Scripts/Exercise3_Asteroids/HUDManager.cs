using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI menuText;

    public bool IsPaused { get; private set; }
    [SerializeField] private GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsPaused = false;
        menuText.SetText("Pause");
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("Score: {0:000}", newScore);
    }

    public void UpdateLives(int newLives)
    {
        livesText.SetText("Lives: {0:0}", newLives);
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        pauseMenu.SetActive(IsPaused);
    }

    public void Unpause()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOverScreen(int finalScore)
    {
        pauseMenu.SetActive(true);
        menuText.SetText("GAME OVER\nScore: {0:000}", finalScore);
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