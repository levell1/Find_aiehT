using UnityEngine;

public class PlayerExpBar : PlayerBaseSlider
{ 
    [SerializeField] private PlayerExpSystem _playerExpSystem;
    private new void Awake()
    {
        if (_playerExpSystem == null)
        {
            _playerExpSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();
        }
        base.Awake();
        _playerExpSystem.OnChangeExpUI += base.ChangeBar;
    }

}
