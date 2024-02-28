using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

}

[Serializable]
public class SavePlayerData

{
    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    private PlayerExpSystem _expSystem;
    private PlayerSO _playerData;
    private EquipmentDatas _equipmentDatas;
    private GlobalTimeManager _globalTimeManager;
    private Inventory _inventory;
    private PotionInventory _potionInventory;
    private QuestManager _questManager;
    private DataManager _dataManager;
    private SoundManager _soundManager;

    private Player _player;

    [Header("Scene")]
    [HideInInspector] public string CurrentSceneName;

    [Header("PlayerSO")]
    [HideInInspector] public PlayerData SaveData;

    [Header("PlayerCurrenState")]
    [HideInInspector] public float SaveHealth;
    [HideInInspector] public float SaveStamina;
    [HideInInspector] public int SavePlayerLevel;
    [HideInInspector] public int SavePlayerExp;
    [HideInInspector] public int SavePlayerGold;

    [Header("PlayerPosition")]
    [HideInInspector] public SerializableVector3 SavePlayerPosition;

    [Header("Equipment")]
    [HideInInspector] public int[] SaveEquipLevel = new int[6];

    [Header("FieldUniqueEnemy")]
    [HideInInspector] public Dictionary<int, bool> SaveBossCheck = new Dictionary<int, bool>();

    [Header("GlobalTime")]
    [HideInInspector] public float SaveDay;
    [HideInInspector] public float SaveHour;
    [HideInInspector] public float SaveMinutes;
    [HideInInspector] public float SaveDayTime;


    [Header("Inventory")]
    [HideInInspector] public Dictionary<int, int> SaveInventoryItems = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SavePotions = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SaveQuickSlotPotions = new Dictionary<int, int>();



    [Header("Quest")]
    [HideInInspector] public List<int> SaveAcceptQuestID = new List<int>();
    [HideInInspector] public Dictionary<int, int> SaveAcceptQuest = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SaveActiveQuest = new Dictionary<int, int>();

    [HideInInspector] public Dictionary<int, int> SaveEnemyQuestProgress = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SaveNatureQuestProgress = new Dictionary<int, int>();

    [HideInInspector] public Dictionary<int, int> SaveEnemyQuestReward = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SaveNatureQuestReward = new Dictionary<int, int>();

    [Header("MainQuest")]
    [HideInInspector] public Dictionary<int, int> SaveActiveMainQuest = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, bool> SaveActiveMainQuestProgress = new Dictionary<int, bool>();


    [Header("SoundVolume")]
    [HideInInspector] public float MasterSoundVolume;
    [HideInInspector] public float BGMSoundVolume;
    [HideInInspector] public float SFXSoundVolume;

    public void SetData(GameObject playerObject)
    {
        
        _player = GameManager.Instance.Player.GetComponent<Player>();
        _playerData = playerObject.GetComponent<Player>().Data;
        _healthSystem = playerObject.GetComponent<HealthSystem>();
        _staminaSystem = playerObject.GetComponent<StaminaSystem>();
        _expSystem = playerObject.GetComponent<PlayerExpSystem>();
        _equipmentDatas = playerObject.GetComponent<EquipmentDatas>();
        _potionInventory = playerObject.GetComponent<PotionInventory>();
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _soundManager = GameManager.Instance.SoundManager;

        _inventory = _player.GetComponent<Inventory>();

        _questManager = GameManager.Instance.QuestManager;
        _dataManager = GameManager.Instance.DataManager;
    }

    public void SavePlayer()
    {

        ClearList();

        SavePlayerSO();
        SaveSceneData();
        SavePlayerCurrentStateData();
        SavePlayerPositionData();
        SavePlayerEquipData();
        SaveGlobalTimeData();
        SavePlayerInventoryData();
        SavePlayerQuestData();
        SaveMainQuest();
        SaveBossDeadCheck();
        SaveSoundVolumeData();

    }

    private void ClearList()
    {
        SaveInventoryItems.Clear();
        SavePotions.Clear();
        SaveQuickSlotPotions.Clear();

        SaveAcceptQuestID.Clear();
        SaveAcceptQuest.Clear();
        SaveEnemyQuestProgress.Clear();
        SaveNatureQuestProgress.Clear();
        SaveActiveQuest.Clear();

        SaveActiveMainQuest.Clear();
        SaveActiveMainQuestProgress.Clear();
        SaveNatureQuestReward.Clear();
        SaveEnemyQuestReward.Clear();
        SaveBossCheck.Clear();
        
    }

    public void SaveSceneData()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    public void SavePlayerSO()
    {
        SaveData.PlayerName = _playerData.PlayerData.PlayerName;
        SaveData.PlayerLevel = _playerData.PlayerData.PlayerLevel;
        SaveData.PlayerMaxHealth = _playerData.PlayerData.PlayerMaxHealth;
        SaveData.PlayerMaxStamina = _playerData.PlayerData.PlayerMaxStamina;
        SaveData.PlayerAttack = _playerData.PlayerData.PlayerAttack;
        SaveData.PlayerDef = _playerData.PlayerData.PlayerDef;
        SaveData.PlayerExp = _playerData.PlayerData.PlayerExp;
        SaveData.PlayerGold = _playerData.PlayerData.PlayerGold;
    }

    public PlayerData InitLoadPlayerData()
    {
        return SaveData;
    }

    public void SavePlayerCurrentStateData()
    {

        SaveHealth = _healthSystem.Health;
        SaveStamina = _staminaSystem.Stamina;
        SavePlayerLevel = _expSystem.PlayerLevel;
        SavePlayerExp = _expSystem.PlayerExp;
        SavePlayerGold = _playerData.PlayerData.PlayerGold;
    }

    public void SavePlayerPositionData()
    {
        SavePlayerPosition = new SerializableVector3(_player.transform.position);
    }

    public void SavePlayerEquipData()
    {
        for (int i = 0; i < _equipmentDatas.EquipData.Length; i++)
        {
            SaveEquipLevel[i] = _equipmentDatas.EquipData[i].Level;
        }

    }

    public void SaveGlobalTimeData()
    {
        SaveDay = _globalTimeManager.Day;
        SaveHour = _globalTimeManager.Hour;
        SaveMinutes = _globalTimeManager.Minutes;
        SaveDayTime = _globalTimeManager.DayTime;
    }

    public void SavePlayerInventoryData()
    {

        foreach (ItemSlot slot in _inventory.Slots)
        {
            SaveInventoryItems.Add(slot.Item.ItemID, slot.Quantity);
        }

        foreach (PotionInventorySlot potionSlot in _potionInventory.Potions)
        {
            SavePotions.Add(potionSlot.PotionSO.ID, potionSlot.InitQuantity);
        }

        SaveQuickSlotPotions.Add(_potionInventory.HPPotionQuick.PotionSO.ID, _potionInventory.HPPotionQuick.Quantity);
        SaveQuickSlotPotions.Add(_potionInventory.SPPotionQuick.PotionSO.ID, _potionInventory.SPPotionQuick.Quantity);
    }

    public void SavePlayerQuestData()
    {

        foreach (Quest quest in _questManager.AcceptQuestList)
        {
            SaveAcceptQuestID.Add(quest.QuestNumber);
            SaveAcceptQuest.Add(quest.TargetID, quest.TargetQuantity);
        }

        foreach (Quest quest in _questManager.ActiveDailyQuests)
        {
            SaveActiveQuest.Add(quest.TargetID, quest.TargetQuantity);
        }

        foreach (NatureDailyQuest quest in _questManager.ActiveDailyQuests.OfType<NatureDailyQuest>())
        {
            SaveNatureQuestReward.Add(quest.QuestNumber, quest.NatureToalQuestReward);
        }

        foreach (EnemyDailyQuest quest in _questManager.ActiveDailyQuests.OfType<EnemyDailyQuest>())
        {
            SaveEnemyQuestReward.Add(quest.QuestNumber, quest.EnemyTotalQuestReward);
        }

        foreach (var quest in _questManager.EnemyQuantityDict)
        {
            SaveEnemyQuestProgress.Add(quest.Key, quest.Value);
        }

        foreach (var quest in _questManager.NatureQuantityDict)
        {
            SaveNatureQuestProgress.Add(quest.Key, quest.Value);
        }

    }

    public void SaveMainQuest()
    {
        foreach (var quest in _questManager.MainQuestQuantityDict)
        {
            SaveActiveMainQuest.Add(quest.Key, quest.Value);
        }

        foreach (var quest in _questManager.ActiveMainQuests)
        {
            SaveActiveMainQuestProgress.Add(quest.QuestNumber, quest.IsProgress);
        }

    }

    public void SaveBossDeadCheck()
    {
        foreach (var BossCheck in _dataManager.BossDeadCheckDict)
        {
            SaveBossCheck.Add(BossCheck.Key, BossCheck.Value);
        }
    }

    public void SaveSoundVolumeData()
    {
        _soundManager.GetMasterVolume(out MasterSoundVolume);
        _soundManager.GetSFXVolume(out SFXSoundVolume);
        _soundManager.GetMusicVolume(out BGMSoundVolume);
        PlayerPrefs.SetFloat("MasterSoundVolume", MasterSoundVolume);
        PlayerPrefs.SetFloat("SFXSoundVolume", SFXSoundVolume);
        PlayerPrefs.SetFloat("BGMSoundVolume", BGMSoundVolume);
    }
}


public class SaveDataManager : MonoBehaviour
{
    public SavePlayerData SaveplayerData = new SavePlayerData();

    private void Start()
    {
        SaveplayerData.SetData(GameManager.Instance.Player);
    }

    public void SavePlayerDataToJson()
    {
        SaveDatas();

        GameManager.Instance.JsonReaderManager.SaveJson(SaveplayerData, JsonDataName.SaveFile);
    }

    private void SaveDatas()
    {
        SaveplayerData.SavePlayer();
    }

}