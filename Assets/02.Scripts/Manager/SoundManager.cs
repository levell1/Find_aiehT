using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClip;
    public AudioSource BgSound;
    public AudioMixer Mixer;
    private string _bgFilename;

    private float _soundValue;


    private void Awake()
    {
        _audioClip = GetComponent<AudioClip>();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;

        BgSoundPlay("BG1", 0.05f);
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "KJW")
        {
            _bgFilename = "";
        }
        else if (scene.name == "99.BJH")
        {
            _bgFilename = "BG3";
        }
        BgSoundPlay(_bgFilename, 0.05f);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioGo = new GameObject(sfxName + "Sound");
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();

        //audiosource.outputAudioMixerGroup = Mixer.FindMatchingGroups("SFX")[0];
        _audioClip = Resources.Load<AudioClip>("Sound/SFX/" + sfxName);
        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.volume = audioVolume;
            audiosource.Play();

            Destroy(audiosource.gameObject, audiosource.clip.length);
        }

    }


    public void BgSoundPlay(string BgName, float audioVolume)
    {
        _audioClip = Resources.Load<AudioClip>("Sound/BGM/" + BgName);
        BgSound.clip = _audioClip;
        //BgSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("BGSound")[0];
        BgSound.loop = true;
        BgSound.volume = audioVolume;
        BgSound.Play();
    }



    //º¼·ýÁ¶Àý
    public void BGSoundVolume(Slider _bgmSlider)
    {
        _soundValue = _bgmSlider.value;
        Mixer.SetFloat("BGVolume", _soundValue);
    }
    public void SFXSoundVolume(Slider _sfxSlider)
    {
        _soundValue = _sfxSlider.value;
        Mixer.SetFloat("SFXVolume", _soundValue);
    }
    public void MasterVolume(Slider _masterSlider)
    {
        _soundValue = _masterSlider.value;
        Mixer.SetFloat("Master", _soundValue);
    }
}
