using UnityEngine;
using System.Collections;
/*
 * Excercise 03.4: Haste.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the Haste power up functionality
 */
public class Haste : PowerUp
{
    [SerializeField] private float hasteTime = 10.0f;
    [SerializeField] private float hasteMultiplier = 2.0f;

    protected override void givePowerUp(AsteroidsPlayerController spaceship)
    {
        StartCoroutine(GiveHaste(spaceship));
    }

    /// <summary>
    /// Gives haste speed to the spaceship
    /// </summary>
    /// <returns></returns>
    public IEnumerator GiveHaste(AsteroidsPlayerController spaceship)
    {
        spaceship.PowerMultiplier = hasteMultiplier;
        yield return new WaitForSeconds(hasteTime);
        spaceship.PowerMultiplier = 1.0f;
        Destroy(gameObject);
    }
}