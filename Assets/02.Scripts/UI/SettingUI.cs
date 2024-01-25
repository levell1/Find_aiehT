using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private Button _checksaveButton;
    [SerializeField] private Button _checkExitButton;
    [SerializeField] private GameObject _savePanel;
    [SerializeField] private GameObject _ExitCheck;
    [SerializeField] private Button _exitButton;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        _savePanel.SetActive(false);
        _ExitCheck.SetActive(false);    
    }
    private void Start()
    {
        _checksaveButton.onClick.AddListener(SaveGame);
        _checkExitButton.onClick.AddListener(ShowExitGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    void SaveGame() 
    {
        ShowOneSecUI(_savePanel,2f);
    }
    void ShowExitGame() 
    {
        _ExitCheck.SetActive(true);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
