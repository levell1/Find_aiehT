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

            //GameManager.instance.SoundManager.SFXPlay("sound", gameObject.transform.position, 0.1f);
            GameManager.instance.SoundManager.BgSoundPlay("BG3");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadingSceneController.LoadScene(SceneName.VillageScene);
            //SceneManager.LoadScene("BJH");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.instance.UIManager.ShowCanvas(UIName.ReforgeUI);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            GameManager.instance.UIManager.CloseLastCanvas();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.UIManager.ShowCanvas(UIName.PlayerStatusUI);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.instance.UIManager.ShowCanvas(UIName.InventoryUI);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameManager.instance.UIManager.ShowCanvas(UIName.RestaurantUI);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.UIManager.ShowCanvas(UIName.PlayerStatusUI);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            //GameManager.instance.UIManager.ShowCanvas(UIName.ShopUI);
        }
    }
}
