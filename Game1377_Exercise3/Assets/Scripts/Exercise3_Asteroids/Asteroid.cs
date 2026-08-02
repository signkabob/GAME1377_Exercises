using UnityEngine;

/*
 * Excercise 03.4: Asteroid.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the asteroid functionality
 */
public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed;
    [SerializeField] private float minRotationSpeed = -180f;
    [SerializeField] private float maxRotationSpeed = 180f;
    [SerializeField] private int numOfSpawnChild = 2;
    [SerializeField] private string explosionAnimationState = "AsteroidExplosionAnim";

    private Rigidbody2D rb;
    private AsteroidSpawner spawner;
    private ScoreManager score;
    private Vector2 velocity;
    private bool hasExploded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // A normalized vector to the edge of the circle in any random direction  
        velocity = Random.insideUnitCircle.normalized;
        // A random rotation speed between minimum and max range
        float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        rb.linearVelocity = velocity * speed;
        rb.angularVelocity = randomRotationSpeed;
    }

    /// <summary>
    /// Breaks down the asteroid into lesser size and destroy it
    /// </summary>
    private void BreakAsteroid()
    {
        score.AddScore(size);
        if (size != AsteroidSize.Small)
        {
            SpawnChildren(size - 1);
        }
        Animator animator = GetComponent<Animator>();
        animator.Play(explosionAnimationState);
        score.updateNumOfAsteroid(-1);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    /// <summary>
    /// Spawns two asteroid spawns of lesser size
    /// </summary>
    /// <param name="childSize">next lesser size of the spawn</param>
    private void SpawnChildren(AsteroidSize childSize)
    {
        for (int i = 0; i < numOfSpawnChild; i++)
        {
            spawner.SpawnAsteroid(transform.position, childSize);
        }
    }

    /// <summary>
    /// Determines the event when colliding with specific objects 
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            AsteroidsPlayerController spaceship = collider.GetComponent<AsteroidsPlayerController>();
            if (spaceship.CurrentState == AsteroidsPlayerController.State.Active && !hasExploded) 
            {
                StartCoroutine(spaceship.KaboomToDeath());
            }
        }

        if (collider.gameObject.CompareTag("Bullet") && !hasExploded)
        {
            // Make sure it no longer collides when the asteroid is destroyed. 
            hasExploded = true;
            BreakAsteroid();
        }
    }

    /// <summary>
    /// Set the spawner reference
    /// </summary>
    /// <param name="asteroidSpawner">Asteroid spawner</param>
    public void SetAsteroidSpawner(AsteroidSpawner asteroidSpawner)
    {
        spawner = asteroidSpawner;
    }

    /// <summary>
    /// Set the score manager
    /// </summary>
    /// <param name="scoreManager">Score manager</param>
    public void SetScoreManager(ScoreManager scoreManager)
    {
        score = scoreManager;
    }
}