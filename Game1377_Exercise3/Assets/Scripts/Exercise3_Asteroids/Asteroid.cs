/*
 * Excercise 03.2: Asteroid.cs
 * Name: Ka Bo Cheung
 * Date: 06/27/2026
 * Course: GAME-1377-001
 * 
 * Script for the asteroid functionality
 */
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Small, Medium, Large }

    [SerializeField] private AsteroidSize size;
    [SerializeField] private float speed;
    [SerializeField] private float minRotationSpeed = -180f;
    [SerializeField] private float maxRotationSpeed = 180f;

    private Rigidbody2D rb;
    private AsteroidSpawner spawner;
    private Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.Find("AsteroidSpawner").GetComponent<AsteroidSpawner>();
        
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
        if (size == AsteroidSize.Large)
        {
            SpawnChildren(AsteroidSize.Medium);
        }
        else if (size == AsteroidSize.Medium)
        {
            SpawnChildren(AsteroidSize.Small);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Spawns two asteroid spawns of lesser size
    /// </summary>
    /// <param name="childSize">next lesser size of the spawn</param>
    private void SpawnChildren(AsteroidSize childSize)
    {
        for (int i = 0; i < 2; i++)
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
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("Bullet"))
        {
            BreakAsteroid();
        }
    }
}