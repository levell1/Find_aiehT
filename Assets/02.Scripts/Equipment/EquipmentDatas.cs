using UnityEngine;

public class EquipmentDatas : MonoBehaviour
{
    public EquipmentData[] EquipData = new EquipmentData[6];
    private int[] _loadEquipLevels = new int[6];

    private HealthSystem _healthSystem;

    public float SumHealth;
    public float SumDef;
    public float SumDmg;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        Init();
    }

    private void Init()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;
        
        for (int i = 0; i < 6; i++)
        {
            if (gameStateManager.CurrentGameState == GameState.LOADGAME)
            {
                _loadEquipLevels[i] = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveEquipLevel[i];
                EquipData[i].Level = _loadEquipLevels[i];
                SetEquipCurrent(i);
            }
            else if(gameStateManager.CurrentGameState == GameState.NEWGAME)
            {
                EquipData[i].Level = 0;
                SetEquipCurrent(i);
            }
        }
        SumEquipStat();
    }

    public void EquipLevelUp(int i) 
    {
        EquipData[i].Level++;
        SetEquipCurrent(i);
        SumEquipStat();
        _healthSystem.SetMaxHealth();
    }
    public void SetEquipCurrent(int i) 
    {
        int equipgold = EquipData[i].Equipment.UpgradeGold; 
        int equiphealth = EquipData[i].Equipment.EquipmentHealth;
        int equipdef = EquipData[i].Equipment.EquipmentDef; 
        int equipdmg = EquipData[i].Equipment.EquipmentDmg;

        float setvalue = (Mathf.Pow(EquipData[i].Level, 2) / 2);

        EquipData[i].CurrentUpgradeGold = Mathf.Ceil(equipgold + equipgold * setvalue);
        EquipData[i].Currenthealth = Mathf.Ceil(equiphealth + equiphealth * setvalue);
        EquipData[i].CurrentDef = Mathf.Ceil(equipdef + equipdef * setvalue);
        EquipData[i].CurrentAttack = Mathf.Ceil(equipdmg + equipdmg * setvalue);
    }

    public void SumEquipStat() 
    {
        SumHealth = 0;
        SumDef = 0;
        SumDmg = 0;
        for (int i = 0; i < 6; i++)
        {
            SumHealth += EquipData[i].Currenthealth;
            SumDef += EquipData[i].CurrentDef;
            SumDmg += EquipData[i].CurrentAttack;
        }
    }
}
