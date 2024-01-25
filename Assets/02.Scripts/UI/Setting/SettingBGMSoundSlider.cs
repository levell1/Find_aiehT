using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBGMSoundSlider : MonoBehaviour
{
    private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
    }
    void Start()
    {
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        if (gameObject.name == "MasterSlider")
        {
            GameManager.instance.SoundManager.SetMasterVolume(volume);
        }
        else if (gameObject.name == "BGMSlider")
        {
            GameManager.instance.SoundManager.SetMusicVolume(volume);
        }
        else if (gameObject.name == "SFXSlider") 
        {
            GameManager.instance.SoundManager.SetSFXVolume(volume);
        }
    }

}
