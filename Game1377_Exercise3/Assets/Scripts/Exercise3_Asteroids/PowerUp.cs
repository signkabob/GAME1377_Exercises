using UnityEngine;

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
    private bool hasTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When colliding with certain power ups
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            GetComponent<SpriteRenderer>().enabled = false;
            AsteroidsPlayerController spaceship = collision.GetComponent<AsteroidsPlayerController>();
            givePowerUp(spaceship);
        }
    }

    protected virtual void givePowerUp(AsteroidsPlayerController spaceship)
    {
        Debug.Log("Give power up!");
    }
}