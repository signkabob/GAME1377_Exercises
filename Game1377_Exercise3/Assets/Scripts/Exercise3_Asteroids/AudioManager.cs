using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

/*
 * Excercise 03.3: AudioManager.cs
 * Name: Ka Bo Cheung
 * Date: 07/20/2026
 * Course: GAME-1377-001
 * 
 * Script for the audio manager
 */
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip thrustSound;
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private AudioClip explosionSound;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays firing blast sound
    /// </summary>
    public void PlayFireSound()
    {
        audioSource.PlayOneShot(fireSound);
    }
    /// <summary>
    /// Plays thrusting booster sound
    /// </summary>
    public void PlayThrustSound()
    {
        // Because thrust is used during Update(), the sound needs not to be overlay 
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
    }
    /// <summary>
    /// Plays teleporting sound
    /// </summary>
    public void PlayTeleportSound()
    {
        audioSource.PlayOneShot(teleportSound);
    }

    /// <summary>
    /// Plays explosion sound on death
    /// </summary>
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }

}
