using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Excercise 03.4: ScoreManager.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for keeping tracking of the scores and such
 */
public class ScoreManager : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int NumOfLives { get; set; } = 3;
    public int NumOfAsteroids { get; set; } = 0;

    [SerializeField] private int[] sizePoints = { 100, 50, 20 }; // score points for { Small, Medium, Large }

    [SerializeField] private HUDManager hud;

    void Start()
    {
        if (hud == null)
        {
            hud = GameManager.Instance.HUDManager;
        }
        hud.UpdateScore(Score);
        hud.UpdateLives(NumOfLives);
    }

    /// <summary>
    /// Add to the score by adding the number of points based on size
    /// </summary>
    /// <param name="size">Size of the destroyed asteroid</param>
    public void AddScore(Asteroid.AsteroidSize size)
    {
        Score += sizePoints[(int)size];
        hud.UpdateScore(Score);
    }

    /// <summary>
    /// Update the number of lives by one gain or one loss
    /// </summary>
    /// <param name="num">One life gained or lost</param>
    public void updateNumOfLives(int num)
    {
        NumOfLives += num;
        hud.UpdateLives(NumOfLives);
    }

    /// <summary>
    /// Update the number of asteroids by one new spawn or one destroyed
    /// </summary>
    /// <param name="num">One asteroid spawned or destroyed</param>
    public void updateNumOfAsteroid(int num)
    {
        NumOfAsteroids += num;
        // If there's no more asteroids, the game is over.
        if (NumOfAsteroids <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}