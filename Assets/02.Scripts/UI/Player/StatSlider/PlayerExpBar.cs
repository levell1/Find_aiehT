using UnityEngine;

public class PlayerExpBar : PlayerBaseSlider
{ 
    [SerializeField] private PlayerExpSystem _playerExpSystem;
    private new void Awake()
    {
        if (_playerExpSystem == null)
        {
            _playerExpSystem = GameObject.FindWithTag("Player").GetComponent<PlayerExpSystem>();
        }
        base.Awake();
        _playerExpSystem.OnChangeExpUI += base.ChangeBar;
    }

}
