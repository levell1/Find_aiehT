using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerGold : CommaText
{
    private PlayerSO _playerData;
    private void Awake()
    {
        if (_playerData == null)
        {
            _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
        }
        _playerData.PlayerData.OnGoldUI += base.ChangeGold;
    }
    private void Start()
    {
        base.ChangeGold(_playerData.PlayerData.GetPlayerGold());
    }
}
