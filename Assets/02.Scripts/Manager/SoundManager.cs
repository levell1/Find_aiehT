using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioClip _audioClip;
    private AudioMixer _mixer;
    private string _bgFilename;

    private Queue<AudioSource> _bgmQueue = new Queue<AudioSource>();

    private Coroutine _coroutine = null;


    private void Awake()
    {
        _audioClip = GetComponent<AudioClip>();
        _mixer = Resources.Load<AudioMixer>("Sound/AudioMixer");
    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        BgSoundPlay("BG1");
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName.TitleScene)
        {
            _bgFilename = "BG1";
        }
        else if (scene.name == SceneName.VillageScene)
        {
            _bgFilename = "BG3";
        }
        else if (scene.name == SceneName.TycoonScene)
        {
            _bgFilename = "BG3";
        }
        else
        {
            _bgFilename = "BG1";
        }
        BgSoundPlay(_bgFilename);
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioObject = new GameObject(sfxName + "Sound");
        AudioObject.transform.position = audioPosition;
        AudioSource audiosource = AudioObject.AddComponent<AudioSource>();
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

        if (_bgmQueue.Count != 0)
        {
            AudioSource firstAudio = _bgmQueue.Dequeue();
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            StartCoroutine(BgmVolumeDown(firstAudio));
        }

        GameObject AudioGo = new GameObject(BgName + "BGM");
        DontDestroyOnLoad(AudioGo);
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = _mixer.FindMatchingGroups("BGSound")[0];
        _audioClip = Resources.Load<AudioClip>("Sound/BGM/" + BgName);

        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.loop = true;
            _coroutine = StartCoroutine(BgmVolumeUp(audiosource));
            audiosource.Play();
        }
        _bgmQueue.Enqueue(audiosource);

    }
    IEnumerator BgmVolumeDown(AudioSource bgmsource)
    {
        while (bgmsource.volume > 0.04f)
        {
            bgmsource.volume -= 0.03f;
            yield return new WaitForSeconds(0.3f);
            if (bgmsource==null)
            {
                break;
            }
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

    public void SetMasterVolume(float volume)
    {   
        _mixer.SetFloat("MasterSound", volume);
    }

    public void SetMusicVolume(float volume)
    {
        _mixer.SetFloat("BGSound", volume);
    }

    public void SetSFXVolume(float volume)
    {
        _mixer.SetFloat("SFXSound", volume);
    }

}
