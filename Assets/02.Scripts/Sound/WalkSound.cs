using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public void WalkSoundPlay()
    {
        int random = Random.Range(1, 4);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Walk+ random.ToString());
    }
}
