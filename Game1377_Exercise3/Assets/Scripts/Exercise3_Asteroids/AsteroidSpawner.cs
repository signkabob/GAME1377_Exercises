/*
 * Excercise 03.3: AsteroidSpawner.cs
 * Name: Ka Bo Cheung
 * Date: 07/20/2026
 * Course: GAME-1377-001
 * 
 * Script for the asteroid spawner
 */
using NUnit.Framework.Internal;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // These variables determine the spawn area for the asteroids.
    // They are calculated at Start based off of the camera size. 
    private float spawnXMax = 0f;
    private float spawnXMin = 0f;
    private float spawnYMax = 0f;
    private float spawnYMin = 0f;
    private float playerSafeDistance = 3;

    public GameObject[] AsteroidPrefabs;
    [SerializeField] private int numOfAsteroidsToSpawn = 5;

    void Start()
    {
        float screenHalfHeight = Camera.main.orthographicSize;
        float screenHalfWidth = Camera.main.aspect * screenHalfHeight;
        spawnXMax = screenHalfWidth + playerSafeDistance;
        spawnXMin = -screenHalfWidth - playerSafeDistance;
        spawnYMax = screenHalfHeight + playerSafeDistance;
        spawnYMin = -screenHalfHeight - playerSafeDistance;
        SpawnInitialAsteroids();
    }

    /// <summary>
    /// Spawns specific number of astroids in random location away from the player at the start of the game 
    /// </summary>
    private void SpawnInitialAsteroids()
    {
        for (int i = 0; i < numOfAsteroidsToSpawn; i++)
        {
            // spawnXMin < spawnXrandom < -playerSafeDistance < playerSafeDistance < spawnXrandom < spawnXMax  
            float spawnXRandom = Random.Range(spawnXMin, spawnXMax);
            if (spawnXRandom >= 0 && spawnXRandom < playerSafeDistance)
            {
                spawnXRandom = playerSafeDistance;
            }
            else if(spawnXRandom <= 0 && spawnXRandom > -playerSafeDistance)
            {
                spawnXRandom = -playerSafeDistance;
            }

            // spawnYMin < spawnYrandom < -playerSafeDistance < playerSafeDistance < spawnYrandom < spawnYMax  
            float spawnYRandom = Random.Range(spawnYMin, spawnYMax);
            if (spawnYRandom >= 0 && spawnYRandom < playerSafeDistance)
            {
                spawnYRandom = playerSafeDistance;
            }
            else if(spawnYRandom <= 0 && spawnYRandom > -playerSafeDistance)
            {
                spawnYRandom = -playerSafeDistance;
            }

            SpawnAsteroid(new Vector3(spawnXRandom, spawnYRandom, 0), Asteroid.AsteroidSize.Large);
        }
    }

    /// <summary>
    /// Spawn an asteroid at the location specified by position parameter with the size specified by the size parameter.
    /// </summary>
    /// <param name="position">the location of the spawn</param>
    /// <param name="size">the size of the spawn</param>
    public void SpawnAsteroid(Vector3 position, Asteroid.AsteroidSize size)
    {
        GameObject asteroidPrefab = AsteroidPrefabs[(int) size];
        GameObject asteroidSpawn = Instantiate(asteroidPrefab, position, asteroidPrefab.transform.rotation);
        asteroidSpawn.GetComponent<Asteroid>().SetAsteroidSpawner(this);
    }
}