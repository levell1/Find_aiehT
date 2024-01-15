using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClip;
    private AudioMixer _mixer;
    private string _bgFilename;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private Queue<AudioSource> _bgmqueue = new Queue<AudioSource>();

    private float _soundValue;

    Coroutine coroutine = null;


    private void Awake()
    {
        _audioClip = GetComponent<AudioClip>();
        _mixer = Resources.Load<AudioMixer>("Sound/AudioMixer");
        DontDestroyOnLoad(this);
        
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;

        BgSoundPlay("BG1");
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
        BgSoundPlay(_bgFilename);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioGo = new GameObject(sfxName + "Sound");
        AudioGo.transform.position = audioPosition;
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = _mixer.FindMatchingGroups("SFX")[0];
        _audioClip = Resources.Load<AudioClip>("Sound/SFX/" + sfxName);

        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.volume = audioVolume;
            audiosource.Play();

            Destroy(audiosource.gameObject, audiosource.clip.length);
        }

    }


    public void BgSoundPlay(string BgName)
    {

        if (_bgmqueue.Count != 0)
        {
            AudioSource firstAudio = _bgmqueue.Dequeue();
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            StartCoroutine(BgmVolumeDown(firstAudio));
        }

        GameObject AudioGo = new GameObject(BgName + "BGM");
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = _mixer.FindMatchingGroups("BGSound")[0];
        _audioClip = Resources.Load<AudioClip>("Sound/BGM/" + BgName);

        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.loop = true;
            coroutine = StartCoroutine(BgmVolumeUp(audiosource));
            audiosource.Play();
        }
        _bgmqueue.Enqueue(audiosource);

    }
    IEnumerator BgmVolumeDown(AudioSource bgmsource)
    {
        while (bgmsource.volume > 0.04f)
        {
            bgmsource.volume -= 0.03f;
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(bgmsource.gameObject);
    }
    IEnumerator BgmVolumeUp(AudioSource bgmsource)
    {
        bgmsource.volume = 0;
        while (bgmsource.volume < 0.3f)
        {
            bgmsource.volume += 0.03f;
            yield return new WaitForSeconds(0.4f);
        }
    }


    //볼륨조절

    public void SetMasterVolume(float volume)
    {
        _mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        _mixer.SetFloat("BGVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        _mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

}
