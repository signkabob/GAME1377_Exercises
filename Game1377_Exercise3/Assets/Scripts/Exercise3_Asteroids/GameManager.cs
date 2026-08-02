using UnityEngine;

/*
 * Excercise 03.4: GameManager.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the game manager 
 */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private ScoreManager scoreManager;
    public HUDManager HUDManager;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        if (scoreManager != null)
        {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }

        if (HUDManager != null)
        {
            HUDManager = FindAnyObjectByType<HUDManager>();
        }
    }

    /// <summary>
    /// Trigger the game over state
    /// </summary>
    public void TriggerGameOver()
    {
        IsGameOver = true;
        HUDManager.GameOverScreen(scoreManager.Score);
    }
}
