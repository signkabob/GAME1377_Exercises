using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int NumOfLives { get; set; } = 3;

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
    public void AddScore(Asteroid.AsteroidSize size)
    {
        Score += sizePoints[(int)size];
        Debug.Log(Score);
        hud.UpdateScore(Score);
    }

    public void updateNumOfLives(int num)
    {
        NumOfLives += num;
        hud.UpdateLives(NumOfLives);
    }
}