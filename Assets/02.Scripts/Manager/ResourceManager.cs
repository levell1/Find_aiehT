using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    private AudioClip LoadSoundResource(string ss) 
    {
        return Resources.Load<AudioClip>("Sound/SFX/" + ss);
    }

    public GameObject LoadPrefab(string ss)
    {
        return Resources.Load<GameObject>("Prefabs/" + ss);
    }

    //제네릭 사용해보기.
}
