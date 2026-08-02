using UnityEngine;

/*
 * Excercise 03.4: PowerUp.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the general power up functionality and inheritance
 */
public class PowerUp : MonoBehaviour
{
    private bool hasTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When colliding with the player, give the power up
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            // Make sure the collision with the player only happens one time
            hasTriggered = true;
            GetComponent<SpriteRenderer>().enabled = false;
            AsteroidsPlayerController spaceship = collision.GetComponent<AsteroidsPlayerController>();
            givePowerUp(spaceship);
        }
    }

    /// <summary>
    /// General power up description 
    /// </summary>
    /// <param name="spaceship"></param>
    protected virtual void givePowerUp(AsteroidsPlayerController spaceship)
    {
        Debug.Log("Give power up!");
    }
}