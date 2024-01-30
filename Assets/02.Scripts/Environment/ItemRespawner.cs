using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> ItemWaitSpawnList = new List<GameObject>();

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "KGM") //사냥터
        {
            DontReSpwanItem();
            ReSpwanItem();
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

    private void ReSpwanItem()
    {
        if (GameManager.instance.GlobalTimeManager.ItemReSpawnTime())
        {
            for (int i = 0; i < ItemWaitSpawnList.Count; i++)
            {
                ItemWaitSpawnList[i].SetActive(true);
            }
            ItemWaitSpawnList.Clear();
            GameManager.instance.GlobalTimeManager.IsItemRespawn = true;
        }
    }
}
