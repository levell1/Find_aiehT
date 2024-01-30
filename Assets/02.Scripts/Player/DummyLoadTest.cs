using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyLoadTest : MonoBehaviour
{
    public GameObject Shop;
    public GameObject Inventory;
    public ShopInteraction shopInteraction;
    
    void Update()
    {

        if(Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene(SceneName.TycoonScene);
        }
        else if(Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene("BJH");
        }
        else if( Input.GetKeyDown("z"))
        {
            Shop.SetActive(true);
            Inventory.SetActive(true);
            shopInteraction.SetShop();
        }
        else if(Input.GetKeyDown("v"))
        {
            Inventory.SetActive(true);
        }
        
    }
}
