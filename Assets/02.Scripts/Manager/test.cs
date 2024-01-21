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

            GameObject a = GameManager.instance.ResourceManager.Load<GameObject>("Prefabs/Sphere");
            GameManager.instance.ResourceManager.Instantiate("Prefabs/Sphere");
            Instantiate(a);
            //GameManager.instance.SoundManager.SFXPlay("sound", gameObject.transform.position, 0.1f);
            GameManager.instance.SoundManager.BgSoundPlay("BG3");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            GameManager.instance.UIManager.CloseLastCanvas();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.UIManager.ShowCanvas("ShopUI");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.UIManager.ShowCanvas("InventoryUI");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.UIManager.ShowCanvas("StatusUI");
        }
    }
}
