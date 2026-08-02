/*
 * Excercise 03.4: LifeUp.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the Life Up power up functionality
 */
public class LifeUp : PowerUp
{
    /// <summary>
    /// Give one life up to the player
    /// </summary>
    /// <param name="spaceship"></param>
    protected override void givePowerUp(AsteroidsPlayerController spaceship)
    {
        spaceship.GiveOneLifeUp();
        Destroy(gameObject);
    }
}