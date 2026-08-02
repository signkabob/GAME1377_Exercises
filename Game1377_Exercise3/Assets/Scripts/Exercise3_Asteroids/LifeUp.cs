public class LifeUp : PowerUp
{
    protected override void givePowerUp(AsteroidsPlayerController spaceship)
    {
        spaceship.GiveOneLifeUp();
    }
}