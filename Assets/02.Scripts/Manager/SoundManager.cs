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
    private Dictionary<string, AudioClip> _soundDictionary = new Dictionary<string, AudioClip>();

    private Coroutine _coroutine = null;

    public GameObject Prefabs;
    public int Size;
    public Transform SpawnPoint;


    private Queue<GameObject> _poolObject;



    private void Awake()
    {
        _audioClip = GetComponent<AudioClip>();
        _mixer = Resources.Load<AudioMixer>("Sound/AudioMixer");
        intit();
    }

    private void Start()
    {
        var pre = Resources.LoadAll<AudioClip>("Sound");
        foreach (var p in pre)
        {
            _soundDictionary.Add(p.name, p);
        }
        SceneManager.sceneLoaded += LoadedsceneEvent;
        BgmSoundPlay("TitleBGM");
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName.VillageScene)
        {
            _bgFilename = BGMSoundName.VillageBGM1;
        }
        else if (scene.name == SceneName.TycoonScene)
        {
            _bgFilename = BGMSoundName.TycoonBGM1;
        }
        else if (scene.name == SceneName.DungeonScene)
        {
            _bgFilename = BGMSoundName.DungeonBGM;
        }
        else if (scene.name == SceneName.HuntingScene)
        {
            _bgFilename = BGMSoundName.HuntingField;
        }
        if (_bgFilename != null)
        {
            BgmSoundPlay(_bgFilename);
        }
        _bgFilename = null;
    }

    public void SFXPlay(string sfxName, Vector3 audioPosition, float audioVolume)
    {
        GameObject AudioObject = GetSoundObject();
        AudioSource audiosource = AudioObject.GetComponent<AudioSource>();
        _audioClip = _soundDictionary[sfxName];

        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.volume = audioVolume;
            audiosource.Play();

            StartCoroutine(SFXStop(AudioObject, audiosource));
        }

    }
    public void SFXPlay(string sfxName)
    {
        GameObject AudioObject = GetSoundObject();
        AudioSource audiosource = AudioObject.GetComponent<AudioSource>();
        _audioClip = _soundDictionary[sfxName];

        if (_audioClip != null)
        {
            audiosource.clip = _audioClip;
            audiosource.volume = 0.5f;
            audiosource.Play();

            StartCoroutine(SFXStop(AudioObject, audiosource));
        }

    }
    IEnumerator SFXStop(GameObject AudioObject, AudioSource audiosource)
    {
        yield return new WaitForSecondsRealtime(audiosource.clip.length);
        ReturnSoundObject(AudioObject);
    }


    public void BgmSoundPlay(string BgName)
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

        GameObject AudioGo = new GameObject(BgName);
        DontDestroyOnLoad(AudioGo);
        AudioSource audiosource = AudioGo.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = _mixer.FindMatchingGroups("BGSound")[0];
        _audioClip = _soundDictionary[BgName];

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
            if (bgmsource == null)
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

    public void GetMasterVolume(out float volume)
    {
        _mixer.GetFloat("MasterSound",out volume);

    }

    public void GetMusicVolume(out float volume)
    {
        _mixer.GetFloat("BGSound", out volume);
    }

    public void GetSFXVolume(out float volume)
    {
        _mixer.GetFloat("SFXSound", out volume);
    }

    public void intit()
    {
        _poolObject = new Queue<GameObject>();
        for (int i = 0; i < Size; i++)
        {
            var newObj = Instantiate(Prefabs, SpawnPoint);
            newObj.gameObject.SetActive(false);
            _poolObject.Enqueue(newObj);
        }
    }

    public void ReturnSoundObject(GameObject obj)
    {
        _poolObject.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetSoundObject()
    {
        GameObject obj = _poolObject.Dequeue();
        obj.SetActive(true);
        return obj;
    }

}


