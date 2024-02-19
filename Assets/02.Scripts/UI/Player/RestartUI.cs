using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class RestartUI : BaseUI
{
    private SceneMoveUI _sceneMoveUI;

    private void Start()
    {
        _sceneMoveUI = GameManager.Instance.UIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoVillageBtn()
    {
        _sceneMoveUI.CurrentSceneName = SceneName.VillageScene;
        _sceneMoveUI.Description.text = "마을로 돌아갑니다.";
        _sceneMoveUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GoTitleBtn()
    {
        _sceneMoveUI.CurrentSceneName = SceneName.TitleScene;
        _sceneMoveUI.Description.text = "타이틀로 돌아갑니다.";
        _sceneMoveUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
