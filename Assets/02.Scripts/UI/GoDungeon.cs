using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoDungeon : BaseUI
{
    [SerializeField] private PlayerSO _playerData;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private GameObject _popup;
    [SerializeField] private GameObject _textArea;
    private CommaText _commaText;
    private int _gold;

    //public event Action<int> OnEnterDungeon;
    private void Awake()
    {
        _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
        _commaText = _goldText.gameObject.GetComponent<CommaText>();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == SceneName.HuntingScene)
        {
            _gold = 0;
            _textArea.SetActive(false);
        }
        else
        {
            _gold = 2000;
        }
        _commaText.ChangeGold(_gold);
        _popup.SetActive(false);
    }

    public void ClickButton() 
    {
        if (_playerData.PlayerData.PlayerGold < _gold)
        {
            StartCoroutine(ShowPopupForSeconds(_popup, 1f));
        }
        else
        {
            int questID = GameManager.Instance.DataManager.QuestDataList.MainQuestData[1].QuestID;
            _playerData.PlayerData.PlayerGold= _playerData.PlayerData.PlayerGold - _gold;
            GameManager.Instance.SaveDataManger.SavePlayerDataToJson();

            GameManager.Instance.Player.transform.position = new Vector3(0, 0, 0);
            LoadingSceneController.LoadScene(SceneName.DungeonScene);
            base.CloseUI();
        }
    }
}
