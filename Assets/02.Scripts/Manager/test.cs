using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    private Dictionary<string, GameObject> _popupUi = new Dictionary<string, GameObject>();

    private void Awake()
    {
        
    }
    void Start()
    {

        //GameManager.instance.UIManager.


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

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("BJH");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.instance.UIManager.ShowCanvas("ReforgeUI");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            GameManager.instance.UIManager.CloseLastCanvas();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.UIManager.ShowCanvas("RestartUI");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.UIManager.ShowCanvas("InventoryUI");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.UIManager.ShowCanvas("PlayerStatusUI");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.instance.UIManager.ShowCanvas("ShopUI");
        }
    }
}
