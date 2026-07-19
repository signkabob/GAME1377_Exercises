/*
 * Excercise 03.3: SpaceshipController.cs
 * Name: Ka Bo Cheung
 * Date: 07/19/2026
 * Course: GAME-1377-001
 * 
 * Script for the spaceship to thrust forward, change its rotation, and fire bullets
 */
using UnityEngine;
using System.Collections;

public class AsteroidsPlayerController : MonoBehaviour
{
    public enum State { Invalid, Active, Teleporting, Invincible };
    public enum PowerUp { Invalid, Normal, Haste, TripleFire }

    private Rigidbody2D rb;

    public State currentState;
    public PowerUp currentPowerUp;

    [SerializeField] private int numOfLife = 3;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private float playerSafeDistance = 3;
    [SerializeField] private float invincibilityTime = 5.0f;
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private float rotationInput;
    private float thrustInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentState = State.Active;
        currentPowerUp = PowerUp.Normal;
    }

    void Update()
    {
        HandleRotation();
        HandleFire();
        HandleHyperspace();
    }

    void FixedUpdate()
    {
        HandleThrust();
    }

    /// <summary>
    /// Press 'Horizontal' key to rotate the spaceship to the left or right
    /// </summary>
    private void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Press 'Verticial' key to thrust the spaceship forward
    /// </summary>
    private void HandleThrust()
    {
        float thrustInput = Input.GetAxis("Vertical");
        if (thrustInput > 0)
        {
            rb.AddForce(transform.up * thrustInput * thrustForce);
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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


    public void KaboomToDeath()
    {
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

    private void Respawn()
    {
        transform.position = Vector3.zero;
        StartCoroutine(GiveInvincibility());
    }
    private IEnumerator GiveInvincibility()
    {
        currentState = State.Invincible;
        yield return new WaitForSeconds(invincibilityTime);
        currentState = State.Active;
    }
}
