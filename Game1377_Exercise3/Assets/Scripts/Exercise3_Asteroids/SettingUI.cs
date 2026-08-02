using UnityEngine;

/*
 * Excercise 03.4: SettingUI.cs
 * Name: Ka Bo Cheung
 * Date: 08/01/2026
 * Course: GAME-1377-001
 * 
 * Script for the setting UI controls and modifiers 
 */
public class SettingUI : MonoBehaviour
{
    /// <summary>
    /// Change the music volume to specific ratio
    /// </summary>
    /// <param name="amount">Ratio of the volume</param>
    public void SetMusicVolume(float amount)
    {
        AudioManager.Instance.MusicSource.volume = amount;
    }

    /// <summary>
    /// Change the SFX volume to specfic ratio 
    /// </summary>
    /// <param name="amount">Ratio of the volume</param>
    public void SetSFXVolume(float amount)
    {
        AudioManager.Instance.SFXSource.volume = amount;
    }
}
