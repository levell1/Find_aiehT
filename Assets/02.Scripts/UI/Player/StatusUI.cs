using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusUI : BaseUI
{
    private PlayerSO _playerData;
    [SerializeField] private EquipmentBase[] _equipment= new EquipmentBase[6];

    private float _sumequipDef = 0;
    private float _sumequipHealth = 0;
    private float _weponDmg = 0;

    [SerializeField] private EquipmentDatas _equipmentupgrade;
    [Header("기본스탯")]
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerLevel;
    [SerializeField] private TMP_Text _maxHealth;
    [SerializeField] private TMP_Text _maxStamina;
    [SerializeField] private TMP_Text _Attack;
    [SerializeField] private TMP_Text _defence;

    [Header("장비 정보")]
    [SerializeField] private Image[] _equipmentSprite = new Image[6];
    [SerializeField] private TMP_Text[] _equipmentName= new TMP_Text[6];
    
    [SerializeField] private TMP_Text _equipmentHealth;
    [SerializeField] private TMP_Text _equipmentDef;
    [SerializeField] private TMP_Text _weaponDmg;


    [Header("스킬정보")]
    [SerializeField] private TMP_Text _qSkillName;
    [SerializeField] private TMP_Text _qSkillDmg;
    [SerializeField] private TMP_Text _qSkillCool;
    [SerializeField] private TMP_Text _qSkillStamina;
    [SerializeField] private TMP_Text _eSkillName;
    [SerializeField] private TMP_Text _eSkillDmg;
    [SerializeField] private TMP_Text _eSkillCool;
    [SerializeField] private TMP_Text _eSkillStamina;

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

    private void OnEnable()
    {
        _sumequipDef = 0;
        _sumequipHealth = 0;
        _weponDmg = 0;

        _playerName.text = _playerData.PlayerData.GetPlayerName();
        _playerLevel.text = _playerData.PlayerData.GetPlayerLevel().ToString();
        _maxHealth.text = _playerData.PlayerData.GetPlayerMaxHealth().ToString();
        _maxStamina.text = _playerData.PlayerData.GetPlayerMaxStamina().ToString();
        _Attack.text = _playerData.PlayerData.GetPlayerAtk().ToString();
        _defence.text = _playerData.PlayerData.GetPlayerDef().ToString();

        for (int i = 0; i < _equipment.Length; i++)
        {
            _equipmentName[i].text = _equipmentupgrade.EquipData[i].Equipment.Name + "(+" + _equipmentupgrade.EquipData[i].Level.ToString() + ")";
            _equipmentSprite[i].sprite = _equipmentupgrade.EquipData[i].Equipment.sprite;

            _sumequipHealth += _equipmentupgrade.EquipData[i].Currenthealth;
            _sumequipDef += _equipmentupgrade.EquipData[i].CurrentDef;
            _weponDmg += _equipmentupgrade.EquipData[i].CurrentAttack;
        }


        _equipmentHealth.text = _sumequipHealth.ToString();
        _equipmentDef.text = _sumequipDef.ToString();
        _weaponDmg.text = _weponDmg.ToString();

        _qSkillName.text = _playerData.SkillData.SkillInfoDatas[0].GetSkillName()+"(Q)";
        _qSkillDmg.text = _playerData.SkillData.SkillInfoDatas[0].GetSkillDamage().ToString();
        _qSkillCool.text = _playerData.SkillData.SkillInfoDatas[0].GetSkillCoolTime().ToString();
        _qSkillStamina.text = _playerData.SkillData.SkillInfoDatas[0].GetSkillCost().ToString();

        _eSkillName.text = _playerData.SkillData.SkillInfoDatas[1].GetSkillName() + "(E)";
        _eSkillDmg.text = _playerData.SkillData.SkillInfoDatas[1].GetSkillDamage().ToString();
        _eSkillCool.text = _playerData.SkillData.SkillInfoDatas[1].GetSkillCoolTime().ToString();
        _eSkillStamina.text = _playerData.SkillData.SkillInfoDatas[1].GetSkillCost().ToString();
    }

}
