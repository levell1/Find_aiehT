using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChange : MonoBehaviour
{
    protected string bgmName ;
    protected private string beforeBgmName;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagName.Player)
        {
            GameManager.Instance.SoundManager.BgSoundPlay(bgmName);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == TagName.Player)
        {
            GameManager.Instance.SoundManager.BgSoundPlay(beforeBgmName);
        }
    }


}
