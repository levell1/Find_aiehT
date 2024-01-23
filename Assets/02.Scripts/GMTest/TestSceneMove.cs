using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneMove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("GMTestScene");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("KGM");
        }

    }
}
