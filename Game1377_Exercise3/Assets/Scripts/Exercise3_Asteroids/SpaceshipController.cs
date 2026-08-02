/*
 * Excercise 03.3: SpaceshipController.cs
 * Name: Ka Bo Cheung
 * Date: 07/20/2026
 * Course: GAME-1377-001
 * 
 * Script for the spaceship to thrust forward, change its rotation, and fire bullets
 */
using UnityEngine;
using System.Collections;

public class AsteroidsPlayerController : MonoBehaviour
{
    public enum State { Invalid, Active, Teleporting, Dead, Invincible };

    private Rigidbody2D rb;
    private PowerUp powerUp;

    public State CurrentState;
    
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private float playerSafeDistance = 3;

    [SerializeField] private float fireCooldownTime = 1.0f;
    [SerializeField] private bool fireOnCooldown = false;
    public float PowerMultiplier = 1.0f;
    [SerializeField] private float invincibilityTime = 5.0f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    public bool IsOnTripleBlastMode = false;
    [SerializeField] private float sideBlasterCloseness = 0.4f;

    [SerializeField] private Animator spaceshipAnim;
    [SerializeField] private string spaceshipActiveState = "SpaceshipActive";
    [SerializeField] private string spaceshipRespawnState = "SpaceshipRespawnAnim";
    [SerializeField] private string spaceshipTeleportState = "SpaceshipTeleportAnim";
    [SerializeField] private string spaceshipExplosionState = "SpaceshipExplosionAnim";
    [SerializeField] private Animator thrustAnim;
    [SerializeField] private string thrustIdleState = "ThrustIdleAnim";
    [SerializeField] private string thrustMoveState = "ThrustMoveAnim";
    [SerializeField] private Animator gunAnim;
    [SerializeField] private string gunFireState = "GunFireAnim";
    [SerializeField] private Animator hasteAnim;

    [SerializeField] ScoreManager scoreManager;

    private float rotationInput;
    private float thrustInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUp>();
        spaceshipAnim = GetComponent<Animator>();
    }

    void Start()
    {
        CurrentState = State.Active;
    }

    void Update()
    {
        HandlePause();
        if (CurrentState != State.Dead)
        {
            rotationInput = Input.GetAxis("Horizontal");
            thrustInput = Input.GetAxis("Vertical");
            HandleRotation();
            HandleFire();
            HandleHyperspace();
        }
    }

    void FixedUpdate()
    {
        if (CurrentState != State.Dead)
        {
            HandleThrust();
        }
    }

    /// <summary>
    /// Press 'Horizontal' key to rotate the spaceship to the left or right
    /// </summary>
    private void HandleRotation()
    {
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * PowerMultiplier * Time.deltaTime);
    }

    /// <summary>
    /// Press 'Verticial' key to thrust the spaceship forward
    /// </summary>
    private void HandleThrust()
    {
        if (thrustInput > 0)
        {
            rb.AddForce(transform.up * thrustInput * thrustForce * PowerMultiplier);

            // Displays moving animation and sound
            thrustAnim.Play(thrustMoveState);
            AudioManager.Instance.PlayThrustSound();
            
            if (PowerMultiplier > 1.0f) 
            {
                hasteAnim.gameObject.SetActive(true);
            }
        }
        // Displays idle animation when not moving
        else
        {
            thrustAnim.Play(thrustIdleState);
            hasteAnim.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Press Space Bar to fire the bullet
    /// </summary>
    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire"))
        {
            FireBullet();
        }
    }

    private void HandlePause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            HUDManager hudManager = GameManager.Instance.HUDManager;
            if (hudManager.IsPaused)
            {
                GameManager.Instance.HUDManager.Unpause();
            }
            else
            {
                GameManager.Instance.HUDManager.Pause();
            }
        }
    }

    /// <summary>
    /// Instantiate and fire the bullet prefab
    /// </summary>
    private void FireBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned!");
            return;
        }
        if (!fireOnCooldown)
        {
            gunAnim.Play(gunFireState);
            AudioManager.Instance.PlayFireSound();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Ready triple blasters if having the power up 
            if (IsOnTripleBlastMode)
            {
                UseTriBlasters(bulletPrefab, firePoint);
            }
            StartCoroutine(StartFireCooldown());
        }
    }

    /// <summary>
    /// Starts the cooldown for firing the bullet 
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartFireCooldown()
    {
        fireOnCooldown = true;
        yield return new WaitForSeconds(fireCooldownTime);
        fireOnCooldown = false;
    }

    /// <summary>
    /// Set up two new instantiated blasters on the spaceship
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="firePoint"></param>
    private void UseTriBlasters(GameObject bulletPrefab, Transform firePoint)
    {
        Instantiate(bulletPrefab, firePoint.position + (transform.right * sideBlasterCloseness), firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position - (transform.right * sideBlasterCloseness), firePoint.rotation);
    }

    /// <summary>
    /// Press left shift key to hyperspace-teleport the spaceship to random location 
    /// </summary>
    private void HandleHyperspace()
    {
        if (Input.GetButtonUp("Hyperspace") && CurrentState != State.Teleporting)
        {
            TeleportToRandomLocation();
        }
    }
    /// <summary>
    /// Teleport the spaceship to random location within the screen bounds
    /// </summary>
    private void TeleportToRandomLocation()
    {
        float locationX;
        float locationY;
        State previousState = CurrentState;
        CurrentState = State.Teleporting;

        do
        {
            locationX = Random.Range(ScreenBounds.ScreenLeft, ScreenBounds.ScreenRight);
            locationY = Random.Range(ScreenBounds.ScreenBottom, ScreenBounds.ScreenTop);
            if (IsSafeOnScan(new Vector2(locationX, locationY)))
            {
                CurrentState = previousState;
            }
        } while (CurrentState == State.Teleporting);

        // Teleports safely and stop moving
        rb.linearVelocity = Vector3.zero;
        transform.position = new Vector3(locationX, locationY, 0);
        spaceshipAnim.Play(spaceshipTeleportState);
        AudioManager.Instance.PlayTeleportSound();
    }

    /// <summary>
    /// Scans the position if there is any nearby asteroid
    /// </summary>
    /// <param name="scanLocation">location of the scan circle</param>
    /// <returns>True if nearby asteroid is detected. False if otherwise.</returns>
    private bool IsSafeOnScan(Vector2 scanLocation)
    {
        bool safety = true;
        Collider2D hitCollider = Physics2D.OverlapCircle(scanLocation, playerSafeDistance);
        if (hitCollider != null)
        {
            if (hitCollider.CompareTag("Asteroid"))
            {
                safety = false;
            }
        }
        return safety;
    }

    /// <summary>
    /// Gives one more live for the player
    /// </summary>
    public void GiveOneLifeUp()
    {
        scoreManager.updateNumOfLives(1);
    }

    /// <summary>
    /// Destroys the spaceship
    /// </summary>
    /// <returns></returns>
    public IEnumerator KaboomToDeath()
    {
        // The player is dead and should stop moving
        CurrentState = State.Dead;
        rb.linearVelocity = Vector3.zero;
        scoreManager.updateNumOfLives(-1);

        // Must start and finish the explosion animation before the next step
        yield return StartCoroutine(TriggerKaboomAnimation());
        
        // Game over if no lives left
        if (scoreManager.NumOfLives <= 0)
        {
            GameManager.Instance.TriggerGameOver();
            Destroy(gameObject);
        }
        else
        {
            Respawn();
        }
    }

    /// <summary>
    /// Plays the explosion animation and sound
    /// </summary>
    /// <returns></returns>
    private IEnumerator TriggerKaboomAnimation()
    {
        // Start the explosion animation
        AudioManager.Instance.PlayExplosionSound();
        spaceshipAnim.Play(spaceshipExplosionState);

        // Wait for the animation to end; Appeared to be sometime buggy and may be related to multiple sudden collisions with asteroids
        yield return new WaitForSeconds(spaceshipAnim.GetCurrentAnimatorStateInfo(0).length);
    }

    /// <summary>
    /// Respawns the spaceship at the origin and give invincibilty
    /// </summary>
    private void Respawn()
    {
        transform.position = Vector3.zero;
        StartCoroutine(GiveInvincibility());
    }

    /// <summary>
    /// Gives damage immunity for the spaceship for certain amount of time 
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveInvincibility()
    {
        CurrentState = State.Invincible;
        spaceshipAnim.Play(spaceshipRespawnState);
        yield return new WaitForSeconds(invincibilityTime);
        spaceshipAnim.Play(spaceshipActiveState);
        CurrentState = State.Active;
    }
}
