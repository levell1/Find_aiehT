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
    [SerializeField] private Button _keyControlButton;
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
        _keyControlButton.onClick.AddListener(ShowControlKey);
    }

    void SaveGame() 
    {
        ShowOneSecUI(_savePanel,2f);
    }
    void ShowExitGame() 
    {
        _ExitCheck.SetActive(true);
    }
    void ShowControlKey()
    {
        GameManager.instance.UIManager.ShowCanvas(UIName.ControlKeyUI);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
