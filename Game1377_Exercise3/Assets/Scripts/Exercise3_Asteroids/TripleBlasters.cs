using UnityEngine;
using System.Collections;

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
    }
}