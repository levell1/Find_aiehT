using System.Collections;
using TMPro;
using UnityEngine;

public class GoDongeon : BaseUI
{
    [SerializeField] private PlayerSO _playerData;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private GameObject _popup;
    private CommaText _commaText;
    readonly private int _gold= 10000;

    private void Awake()
    {
        _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
        _commaText = _goldText.gameObject.GetComponent<CommaText>();
        _commaText.ChangeGold(_gold);
        _popup.SetActive(false);

    }
    public void ClickButton() 
    {
        if (_playerData.PlayerData.GetPlayerGold()< _gold)
        {
            StartCoroutine(ShowPopupForSeconds(_popup, 1f));
        }
        else
        {
            _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
            _playerData.PlayerData.SetPlayerGold(_playerData.PlayerData.GetPlayerGold() - _gold);
            GameManager.Instance.Player.transform.position = new Vector3(0, 0, 0);
            LoadingSceneController.LoadScene(SceneName.DungeonScene);
            base.CloseUI();
        }
    }
}
