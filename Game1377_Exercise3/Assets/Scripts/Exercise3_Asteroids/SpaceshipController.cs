/*
 * Excercise 03.2: SpaceshipController.cs
 * Name: Ka Bo Cheung
 * Date: 06/25/2026
 * Course: GAME-1377-001
 * 
 * Script for the spaceship to thrust forward, change its rotation, and fire bullets
 */
using UnityEngine;

public class AsteroidsPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float thrustForce = 500f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private float rotationInput;
    private float thrustInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotationInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
        HandleRotation();
        HandleFire();
        HandleHyperspace();
    }

    void FixedUpdate()
    {
        HandleThrust();
    }

    /// <summary>
    /// Press A to rotate the spaceship to the left
    /// Press D to rotate the spaceship to the right
    /// </summary>
    private void HandleRotation()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotationInput * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Press W to thrust the spaceship forward
    /// Press S to thrust the spaceship backward 
    /// </summary>
    private void HandleThrust()
    {
        float thrustInput = Input.GetAxis("Vertical");
        rb.AddForce(transform.up * thrustInput * thrustForce);
    }

    /// <summary>
    /// Press Space Bar to fire the bullet
    /// </summary>
    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire1"))
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    /// <summary>
    /// Press left shift key to hyperspace-teleport the spaceship to random location 
    /// </summary>
    private void HandleHyperspace()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            TeleportToRandomLocation();
        }
    }
    /// <summary>
    /// Teleport the spaceship to random location within the screen bounds
    /// </summary>
    private void TeleportToRandomLocation()
    {
        float locationX = Random.Range(ScreenBounds.ScreenLeft, ScreenBounds.ScreenRight + 1.0f);
        float locationY = Random.Range(ScreenBounds.ScreenBottom, ScreenBounds.ScreenTop + 1.0f);

        transform.position = new Vector3(locationX, locationY, 0);
    }
}
