using UnityEngine;

public class PlayerExpBar : PlayerBaseSlider
{ 
    [SerializeField] private PlayerExpSystem _playerExpSystem;
    private new void Awake()
    {
        base.Awake();
        _playerExpSystem.OnChangeExpUI += base.ChangeBar;
    }

}
