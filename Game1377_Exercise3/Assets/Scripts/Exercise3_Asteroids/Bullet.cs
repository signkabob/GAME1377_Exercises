/*
 * Excercise 03.2: Bullet.cs
 * Name: Ka Bo Cheung
 * Date: 06/25/2026
 * Course: GAME-1377-001
 * 
 * Script for the bullet functionality
 */
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletLifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        // Add instant force to the bullet once instantiated
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

        // Destroy the bullet in a specific time after instantiating
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
