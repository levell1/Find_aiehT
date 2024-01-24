using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReforgeUI : BaseUI
{
    [SerializeField] GameObject _backUI;
    [SerializeField] GameObject _selectUI;
    [SerializeField] GameObject _resultUI;

    [SerializeField] Button[] _selectButton= new Button[6];
    [SerializeField] Image[] _equipimagelist = new Image[6];
    [SerializeField] TMP_Text[] _equiptext= new TMP_Text[6];

    [SerializeField] private EquipmentDatas _equipmentupgrade;
    [SerializeField] private PlayerSO _playerData;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private Image _equipicon;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _plusStatText;

    int _itemnum;

    private void Awake()
    {
        if (_equipmentupgrade == null)
        {
            _equipmentupgrade = GameObject.FindWithTag("Player").GetComponent<EquipmentDatas>();
        }
        if (_playerData == null)
        {
            _playerData = GameObject.FindWithTag("Player").GetComponent<Player>().Data;
        }

    }

    public void ClickUpgradeButton() 
    {
        if (_playerData.PlayerData.GetPlayerGold() > _equipmentupgrade.EquipData[_itemnum].CurrentUpgradeGold) {
            _playerData.PlayerData.SetPlayerGold(_playerData.PlayerData.GetPlayerGold() - (int)_equipmentupgrade.EquipData[_itemnum].CurrentUpgradeGold);
            _equipmentupgrade.EquipLevelUp(_itemnum);
            _resultUI.SetActive(true);
            _resultText.text = "강화 성공";
            
            setImagename();
        }
        else
        {
            _resultUI.SetActive(true);
            _resultText.text = "골드가 부족합니다.";
            
        }
        _selectUI.SetActive(false);
        Invoke("fialuifalse", 1f);
    }

    private void fialuifalse() 
    {
        _resultUI.SetActive(false);
    }
    public void ClickEquip()
    {
        for (int i = 0; i < 6; i++)
        {
            if (EventSystem.current.currentSelectedGameObject.name == _selectButton[i].name)
            {
                Debug.Log(EventSystem.current.currentSelectedGameObject.name);
                Debug.Log(_selectButton[i].name);
                _itemnum = i;   break;
            }
        }
        _goldText.text = _equipmentupgrade.EquipData[_itemnum].CurrentUpgradeGold.ToString()+"골드 필요";
        _equipicon.sprite = _equipmentupgrade.EquipData[_itemnum].Equipment.sprite;
        if (_equipmentupgrade.EquipData[_itemnum].CurrentDef != 0)
        {
            _plusStatText.text = "방어력 + " + (nextstatDef() - _equipmentupgrade.EquipData[_itemnum].CurrentDef);
        }
        else if (_equipmentupgrade.EquipData[_itemnum].Currenthealth != 0)
        {
            _plusStatText.text = "최대체력 + " + (nextstatHealth()-_equipmentupgrade.EquipData[_itemnum].Currenthealth);
        }
        else if (_equipmentupgrade.EquipData[_itemnum].CurrentAttack != 0)
        {
            _plusStatText.text = "공격력 + " + (nextstatDmg()- _equipmentupgrade.EquipData[_itemnum].CurrentAttack);
        }


        _selectUI.SetActive(true);
    }
    private void OnEnable()
    {
        setImagename();
        _selectUI.SetActive(false);
        _resultUI.SetActive(false);
    }
    private float nextstatHealth() 
    {
        return Mathf.Ceil(_equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentHealth + _equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentHealth * (Mathf.Pow(_equipmentupgrade.EquipData[_itemnum].Level+1, 2) / 2));
    }
    private float nextstatDef()
    {
        return Mathf.Ceil(_equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentDef + _equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentDef * (Mathf.Pow(_equipmentupgrade.EquipData[_itemnum].Level + 1, 2) / 2));
    }
    private float nextstatDmg()
    {
        return Mathf.Ceil(_equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentDmg + _equipmentupgrade.EquipData[_itemnum].Equipment.EquipmentDmg * (Mathf.Pow(_equipmentupgrade.EquipData[_itemnum].Level + 1, 2) / 2));
    }
    private void setImagename()
    {
        for (int i = 0; i < 6; i++)
        {
            _equipimagelist[i].sprite = _equipmentupgrade.EquipData[i].Equipment.sprite;
            _equiptext[i].text = _equipmentupgrade.EquipData[i].Equipment.Name + "( +" + _equipmentupgrade.EquipData[i].Level + ")";
        }
    }
}
