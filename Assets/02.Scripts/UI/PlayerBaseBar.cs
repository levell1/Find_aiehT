using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseBar : MonoBehaviour
{
    //public Playerscript Player;
    protected Slider _slider;

    [SerializeField] protected TMP_Text _hpText;

    //플레이어 데이터 가져오기
    // 플레이어 의 수치 변경시 += ChangeHpBar();

    //FIX
    // 테스트용 , Player Data 받아오기
    [SerializeField] protected int _currentValue = 10;
    [SerializeField] protected int _maxValue = 20;


    protected void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected void Start()
    {
        // 체력 변경 시 이벤트 추가  +=ChangeHpBar();
    }
    public void Update()
    {
        //테스트용 업데이트에
        ChangeHpBar();
    }


    // Player Hp 변경 시
    public virtual void ChangeHpBar()
    {
        _hpText.text = (_currentValue + "/" + _maxValue);
        _slider.value = (float)_currentValue / (float)_maxValue;
    }
}
