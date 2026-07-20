using System;
using UnityEngine;
using System.Collections;

/*
 * Excercise 03.3: PowerUp.cs
 * Name: Ka Bo Cheung
 * Date: 07/20/2026
 * Course: GAME-1377-001
 * 
 * Script for the power up functionality
 */
public class PowerUp : MonoBehaviour
{
    private AsteroidsPlayerController spaceship;
    [SerializeField] private float hasteTime = 10.0f;
    [SerializeField] private float hasteMultiplier = 2.0f;
    [SerializeField] private float triBlastTime = 10.0f;
    [SerializeField] private float sideBlasterCloseness = 0.4f;
    public bool IsOnTripleBlastMode = false;

    private void Awake()
    {
        spaceship = GetComponent<AsteroidsPlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When colliding with certain power ups
        if (collision.CompareTag("StarLife"))
        {
            spaceship.GiveOneLifeUp();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SuperEngine"))
        {
            StartCoroutine(GiveHaste());
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("TriBlaster"))
        {
            StartCoroutine(GiveTriBlasters());
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Gives haste speed to the spaceship
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveHaste()
    {
        spaceship.PowerMultiplier = hasteMultiplier;
        yield return new WaitForSeconds(hasteTime);
        spaceship.PowerMultiplier = 1.0f;
    }

    /// <summary>
    /// Gives triple blasters to the spaceship
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveTriBlasters()
    {
        IsOnTripleBlastMode = true;
        yield return new WaitForSeconds(triBlastTime);
        IsOnTripleBlastMode = false;
    }

    /// <summary>
    /// Set up two new instantiated blasters on the spaceship
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="firePoint"></param>
    public void UseTriBlasters(GameObject bulletPrefab, Transform firePoint)
    {
        Instantiate(bulletPrefab, firePoint.position + (transform.right * sideBlasterCloseness), firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position - (transform.right * sideBlasterCloseness), firePoint.rotation);
    }
}
