using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    private AsteroidsPlayerController spaceship;
    [SerializeField] private float hasteTime = 10.0f;
    [SerializeField] private float hasteMultiplier = 2.0f;
    [SerializeField] private float triBlastTime = 10.0f;
    [SerializeField] private float sideBlasterCloseness = 0.4f;
    public bool IsOnTripleBlastMode = false;

    private void Awake()
    {
        spaceship = GetComponent<AsteroidsPlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StarLife"))
        {
            spaceship.GiveOneLifeUp();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("SuperEngine"))
        {
            StartCoroutine(GiveHaste());
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("TriBlaster"))
        {
            StartCoroutine(GiveTriBlasters());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator GiveHaste()
    {
        spaceship.powerMultiplier = hasteMultiplier;
        yield return new WaitForSeconds(hasteTime);
        spaceship.powerMultiplier = 1.0f;
    }

    private IEnumerator GiveTriBlasters()
    {
        IsOnTripleBlastMode = true;
        yield return new WaitForSeconds(triBlastTime);
        IsOnTripleBlastMode = false;
    }

    public void UseTriBlasters(GameObject bulletPrefab, Transform firePoint)
    {
        Instantiate(bulletPrefab, firePoint.position + (transform.right * sideBlasterCloseness), firePoint.rotation);
        Instantiate(bulletPrefab, firePoint.position - (transform.right * sideBlasterCloseness), firePoint.rotation);
    }
}
