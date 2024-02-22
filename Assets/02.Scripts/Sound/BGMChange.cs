using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMChange : MonoBehaviour
{
    [SerializeField] private string bgmName ;
    [SerializeField] private string beforeBgmName;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name==SceneName.VillageScene)
        {
            bgmName = BGMSoundName.VillageBGM2;
            beforeBgmName = BGMSoundName.VillageBGM1;
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagName.Player)
        {
            GameManager.Instance.SoundManager.BgmSoundPlay(bgmName);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagName.Player)
        {
            GameManager.Instance.SoundManager.BgmSoundPlay(beforeBgmName);
        }
    }


}
