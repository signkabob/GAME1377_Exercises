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

    public State currentState;
    
    [SerializeField] private int numOfLife = 3;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private float playerSafeDistance = 3;

    [SerializeField] private float fireCooldownTime = 1.0f;
    [SerializeField] private bool fireOnCooldown = false;
    public float powerMultiplier = 1.0f;
    [SerializeField] private float invincibilityTime = 5.0f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator spaceshipAnim;
    [SerializeField] private Animator thrustAnim;
    [SerializeField] private Animator hasteAnim;
    [SerializeField] private Animator gunAnim;

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
        currentState = State.Active;
    }

    void Update()
    {
        if (currentState != State.Dead)
        {
            HandleRotation();
            HandleFire();
            HandleHyperspace();
        }
    }

    void FixedUpdate()
    {
        if (currentState != State.Dead)
        {
            HandleThrust();
        }
    }

    /// <summary>
    /// Press 'Horizontal' key to rotate the spaceship to the left or right
    /// </summary>
    private void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * powerMultiplier * Time.deltaTime);
    }

    /// <summary>
    /// Press 'Verticial' key to thrust the spaceship forward
    /// </summary>
    private void HandleThrust()
    {
        float thrustInput = Input.GetAxis("Vertical");
        if (thrustInput > 0)
        {
            rb.AddForce(transform.up * thrustInput * thrustForce * powerMultiplier);
            thrustAnim.Play("ThrustMoveAnim");
            if (powerMultiplier > 1.0f) 
            {
                hasteAnim.gameObject.SetActive(true);
            }
        }
        else
        {
            thrustAnim.Play("ThrustIdleAnim");
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
            gunAnim.Play("GunFireAnim");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (powerUp.IsOnTripleBlastMode)
            {
                powerUp.UseTriBlasters(bulletPrefab, firePoint);
            }
            StartCoroutine(StartFireCooldown());
        }
    }

    private IEnumerator StartFireCooldown()
    {
        fireOnCooldown = true;
        yield return new WaitForSeconds(fireCooldownTime);
        fireOnCooldown = false;
    }

    /// <summary>
    /// Press left shift key to hyperspace-teleport the spaceship to random location 
    /// </summary>
    private void HandleHyperspace()
    {
        if (Input.GetButtonUp("Hyperspace") && currentState != State.Teleporting)
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
        State previousState = currentState;
        currentState = State.Teleporting;
        do
        {
            locationX = Random.Range(ScreenBounds.ScreenLeft, ScreenBounds.ScreenRight);
            locationY = Random.Range(ScreenBounds.ScreenBottom, ScreenBounds.ScreenTop);
            if (IsSafeOnScan(new Vector2(locationX, locationY)))
            {
                currentState = previousState;
            }
        } while (currentState == State.Teleporting);
        transform.position = new Vector3(locationX, locationY, 0);
        spaceshipAnim.Play("SpaceshipTeleportAnim");
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
    public void GiveOneLifeUp()
    {
        numOfLife += 1;
    }

    public IEnumerator KaboomToDeath()
    {
        currentState = State.Dead;
        rb.linearVelocity = Vector3.zero;
        yield return StartCoroutine(TriggerKaboomAnimation());
        numOfLife -= 1;

        if (numOfLife <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Respawn();
        }
    }

    private IEnumerator TriggerKaboomAnimation()
    {
        // Start the explosion animation
        thrustAnim.gameObject.SetActive(false);
        hasteAnim.gameObject.SetActive(false);
        gunAnim.gameObject.SetActive(false);
        spaceshipAnim.Play("SpaceshipExplosionAnim");

        // Wait for the next frame just in case the transition isn't completed 
        yield return null;

        // Wait for the animation to end
        yield return new WaitForSeconds(spaceshipAnim.GetCurrentAnimatorStateInfo(0).length);
    }

    private void Respawn()
    {
        transform.position = Vector3.zero;
        thrustAnim.gameObject.SetActive(true);
        gunAnim.gameObject.SetActive(true);
        StartCoroutine(GiveInvincibility());
    }

    private IEnumerator GiveInvincibility()
    {
        currentState = State.Invincible;
        spaceshipAnim.Play("SpaceshipRespawnAnim");
        yield return new WaitForSeconds(invincibilityTime);
        spaceshipAnim.Play("SpaceshipActive");
        currentState = State.Active;
    }
}
