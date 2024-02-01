using UnityEngine;

public class PlayerGold : CommaText
{
    private PlayerSO _playerData;
    private void Awake()
    {
        if (_playerData == null)
        {
            _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
        }
    }
    protected override void Update()
    {
        _Value = _playerData.PlayerData.GetPlayerGold();
        base.Update();
    }
}
