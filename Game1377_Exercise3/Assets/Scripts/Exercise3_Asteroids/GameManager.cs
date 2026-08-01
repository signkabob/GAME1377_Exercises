using UnityEngine;

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
}
