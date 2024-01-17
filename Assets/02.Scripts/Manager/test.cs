using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Dictionary<string, GameObject> _popupUi = new Dictionary<string, GameObject>();

    private void Awake()
    {
        
    }
    void Start()
    {
       

       
       
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            var pre = Resources.LoadAll<GameObject>("Canvas");
            /*foreach (var p in pre) 
            {
                Debug.Log(p.name + "," + p);
                _popupUi.Add(p.name, p);
                Instantiate(p);
            }*/
            GameObject a = GameManager.instance.ResourceManager.Load<GameObject>("Prefabs/Sphere");
            GameManager.instance.ResourceManager.Instantiate("Prefabs/Sphere");
            Instantiate(a);
            //GameManager.instance.SoundManager.SFXPlay("sound", gameObject.transform.position, 0.1f);
            GameManager.instance.SoundManager.BgSoundPlay("BG3");
        }
    }
}
