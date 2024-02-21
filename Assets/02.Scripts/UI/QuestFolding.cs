using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFolding : MonoBehaviour
{
    [SerializeField] private GameObject _onButton;
    [SerializeField] private GameObject _offButton;

    [SerializeField] private GameObject _questList;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (_onButton.activeSelf)
            {
                togglequest(false);
            }
            else
            {
                togglequest(true);
            }
            
        }

    }

    private void togglequest(bool check) 
    {
        _onButton.SetActive(check);
        _offButton.SetActive(!check);
        _questList.SetActive(check);
        
    }

}
