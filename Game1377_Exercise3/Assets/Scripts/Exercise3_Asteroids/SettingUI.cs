using UnityEngine;

public class SettingUI : MonoBehaviour
{
    public void SetMusicVolume(float amount)
    {
        AudioManager.Instance.MusicSource.volume = amount;
    }

    public void SetSFXVolume(float amount)
    {
        AudioManager.Instance.SFXSource.volume = amount;
    }
}
