using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("Score: {0:000}", newScore);
    }

    public void UpdateLives(int newLives)
    {
        livesText.SetText("Lives: {0:0}", newLives);
    }
}
