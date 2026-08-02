using TMPro;
using UnityEngine;

/*
 * Excercise 03.4: HUDManager.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for managing HUD and UI controls
 */
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
    
    /// <summary>
    /// Update the score text
    /// </summary>
    /// <param name="newScore">Updated score</param>
    public void UpdateScore(int newScore)
    {
        scoreText.SetText("Score: {0:000}", newScore);
    }

    /// <summary>
    /// Update the number of lives text
    /// </summary>
    /// <param name="newLives">Updated number of lives</param>
    public void UpdateLives(int newLives)
    {
        livesText.SetText("Lives: {0:0}", newLives);
    }

    /// <summary>
    /// Display the pause screen
    /// </summary>
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

    /// <summary>
    /// Undisplay the pause screen
    /// </summary>
    public void Unpause()
    {
        IsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Display the game over screen
    /// </summary>
    /// <param name="finalScore"></param>
    public void GameOverScreen(int finalScore)
    {
        pauseMenu.SetActive(true);
        menuText.SetText("GAME OVER\nScore: {0:000}", finalScore);
    }
}