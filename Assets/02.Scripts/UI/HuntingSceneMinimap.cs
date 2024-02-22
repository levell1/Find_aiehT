using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingSceneMinimap : MonoBehaviour
{
    [SerializeField] private GameObject _minimapObject;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_minimapObject.activeSelf)
            {
                _minimapObject.SetActive(false);
            }
            else
            {
                _minimapObject.SetActive(true);
            }
        }
    }
}
