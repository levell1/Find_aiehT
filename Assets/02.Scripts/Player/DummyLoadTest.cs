using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DummyLoadTest : MonoBehaviour
{
    
    void Update()
    {

        if(Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene("MWJ");
        }
        else if(Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene("BJH");
        }
        
    }
}
