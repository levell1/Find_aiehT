using UnityEngine;
using UnityEngine.Rendering;
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
        if (PlayerPrefs.HasKey("BGMSoundVolume"))
        {
            if (gameObject.name == "MasterSlider")
            {
                _volumeSlider.value = PlayerPrefs.GetFloat("MasterSoundVolume");
            }
            else if (gameObject.name == "BGMSlider")
            {
                _volumeSlider.value = PlayerPrefs.GetFloat("SFXSoundVolume");
            }
            else if (gameObject.name == "SFXSlider")
            {
                _volumeSlider.value = PlayerPrefs.GetFloat("BGMSoundVolume");
            }
        }
    }


    void OnVolumeChanged(float volume)
    {
        if (gameObject.name == "MasterSlider")
        {
            GameManager.Instance.SoundManager.SetMasterVolume(volume);
        }
        else if (gameObject.name == "BGMSlider")
        {
            GameManager.Instance.SoundManager.SetMusicVolume(volume);
        }
        else if (gameObject.name == "SFXSlider") 
        {
            GameManager.Instance.SoundManager.SetSFXVolume(volume);
        }
    }

}
