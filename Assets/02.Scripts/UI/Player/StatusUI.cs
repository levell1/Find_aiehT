using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusUI : BaseUI
{
    private PlayerSO _playerData;
    [SerializeField] private EquipmentBase[] _equipment= new EquipmentBase[6];

    private int _sumequipDef = 0;
    private int _sumequipHealth = 0;
    private int _weponDmg= 0;

    Weapon weapon;
    Armor armor;

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
            _equipmentName[i].text = _equipment[i].Name + "(+" + _equipment[i].Level.ToString() + ")";
            _equipmentSprite[i].sprite = _equipment[i].sprite;
            if (_equipment[i].type == EquipmentType.Armor)
            {
                armor = _equipment[i] as Armor;
                _sumequipHealth += armor.ItemHealth;
                _sumequipDef += armor.ItemDef;
            }
            if (_equipment[i].type == EquipmentType.Weapon)
            {
                weapon = _equipment[i] as Weapon;
                _weponDmg = weapon.EquipmentAttack;
            }
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
