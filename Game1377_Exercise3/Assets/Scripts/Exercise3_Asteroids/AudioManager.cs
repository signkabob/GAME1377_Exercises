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


    [SerializeField] public AudioSource MusicSource;
    [SerializeField] public AudioSource SFXSource;

    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip thrustSound;
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip buttonSound;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }

    private void PlayBackgroundMusic()
    {
        MusicSource.clip = backgroundMusic;
        MusicSource.loop = true;
        MusicSource.Play();
    }

    /// <summary>
    /// Plays firing blast sound
    /// </summary>
    public void PlayFireSound()
    {
        SFXSource.PlayOneShot(fireSound);
    }
    /// <summary>
    /// Plays thrusting booster sound
    /// </summary>
    public void PlayThrustSound()
    {
        // Because thrust is used during Update(), the sound needs not to be overlay 
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(thrustSound);
        }
    }
    /// <summary>
    /// Plays teleporting sound
    /// </summary>
    public void PlayTeleportSound()
    {
        SFXSource.PlayOneShot(teleportSound);
    }

    /// <summary>
    /// Plays explosion sound on death
    /// </summary>
    public void PlayExplosionSound()
    {
        SFXSource.PlayOneShot(explosionSound);
    }

    public void PlayButtonSound()
    {
        SFXSource.PlayOneShot(buttonSound);
    }
}
