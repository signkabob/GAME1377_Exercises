using UnityEngine;
using System.Collections;

/*
 * Excercise 03.4: TripleBlaster.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the Triple Blasters power up functionality
 */
public class TripleBlasters : PowerUp
{
    [SerializeField] private float triBlastTime = 10.0f;

    protected override void givePowerUp(AsteroidsPlayerController spaceship)
    {
        StartCoroutine(GiveTriBlasters(spaceship));
    }

    /// <summary>
    /// Gives triple blasters to the spaceship
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveTriBlasters(AsteroidsPlayerController spaceship)
    {
        spaceship.IsOnTripleBlastMode = true;
        yield return new WaitForSeconds(triBlastTime);
        spaceship.IsOnTripleBlastMode = false;
        Destroy(gameObject);
    }
}