using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        GameObject a= GameManager.instance.ResourceManager.LoadPrefab("Sphere");
       Instantiate(a);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.SoundManager.SFXPlay("sound", this.transform.position, 0.1f);
            GameManager.instance.SoundManager.BgSoundPlay("BG1", 0.1f);
        }
    }
}
