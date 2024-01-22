using UnityEngine;

public class PlayerGold : CommaText
{
    private PlayerSO _playerData;
    private void Awake()
    {
        if (_playerData == null)
        {
            _playerData = GameObject.FindWithTag("Player").GetComponent<Player>().Data;
        }
    }
    private void Update()
    {
        _Value = _playerData.GetPlayerData().GetPlayerGold();
        base.Update();
    }
}
