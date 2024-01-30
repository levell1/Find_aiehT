using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTest : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _globalTimeManager;
    [SerializeField] private GameObject _mainCam;

    private void Awake()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name!=SceneName.TitleScene)
        {
            _globalTimeManager.SetActive(true);
            _mainCam.SetActive(true);
            _player.SetActive(true);
            _playerUI.SetActive(true);

        }
        if (scene.name == "TycoonScene")
        {
            _globalTimeManager.SetActive(false);
            _playerUI.SetActive(false);

        }
    }
}
