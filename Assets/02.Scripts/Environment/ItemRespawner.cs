using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> ItemWaitSpawnList = new List<GameObject>();

    private void Awake()
    {

    }

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName.Field) //사냥터
        {
            DontReSpwanItem();
        }
    }

    private void DontReSpwanItem()
    {
        if (ItemWaitSpawnList.Count > 0)
        {
            for (int i = 0; i < ItemWaitSpawnList.Count; i++)
            {
                ItemWaitSpawnList[i].SetActive(false);
            }
        }
    }
}

