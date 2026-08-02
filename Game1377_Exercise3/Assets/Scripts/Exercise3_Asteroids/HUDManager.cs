using TMPro;
using UnityEngine;

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
}