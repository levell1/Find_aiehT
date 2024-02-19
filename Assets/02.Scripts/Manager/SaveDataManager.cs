using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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

{  /// 저장하기
   /// 1. 플레이어 현재 데이터 저장
   /// 2. 플레이어 강화 상태 저장
   /// 3. 퀘스트 진행상황 저장
   /// 4. 날짜 및 시간 저장
   /// 5. 플레이어 위치 저장
   /// 6. 플레이어 포션 및 인벤토리 저장 <summary>
/// 저장하기
/// </summary>
    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    private PlayerExpSystem _expSystem;
    private PlayerSO _playerData;
    private EquipmentDatas _equipmentDatas;
    private GlobalTimeManager _globalTimeManager;
    private Inventory _inventory;
    private PotionInventory _potionInventory;
    private QuestManager _questManager;

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
    //[HideInInspector] public Vector3 SavePlayerPosition;

    [Header("Equipment")]
    [HideInInspector] public int[] SaveEquipLevel = new int[6];

    [Header("GlobalTime")]
    [HideInInspector] public float SaveDay;
    [HideInInspector] public float SaveHour;
    [HideInInspector] public float SaveMinutes;
    [HideInInspector] public float SaveDayTime;


    [Header("Inventory")]
    //[HideInInspector] public List<ItemSlot> SaveInventory = new List<ItemSlot>();
    //[HideInInspector] public List<PotionInventorySlot> SavePotionInventory = new List<PotionInventorySlot>();
    
    [HideInInspector] public Dictionary<int, int> SaveInventoryItems = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SavePotions = new Dictionary<int, int>();
    [HideInInspector] public Dictionary<int, int> SaveQuickSlotPotions = new Dictionary<int, int>();

    //[HideInInspector] public int PotionQuantity;

    [Header("Quest")]
    //[HideInInspector] public List<Quest> SaveQuest = new List<Quest>(); // 저장할 퀘스트들 가져옴

    [HideInInspector] public List<int> SaveQuestID = new List<int>();
    [HideInInspector] public Dictionary<int, int> SaveQuest = new Dictionary<int, int>();


    [HideInInspector] public Dictionary<int, int> SaveEnemyQuestProgress = new Dictionary<int, int>(); // 퀘스트 진행상황 가져옴
    [HideInInspector] public Dictionary<int, int> SaveNatureQuestProgress = new Dictionary<int, int>(); // 퀘스트 진행상황 가져옴

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

        _inventory = _player.GetComponent<Inventory>();

        _questManager = GameManager.Instance.QuestManager;
    }

    public void SavePlayer()
    {
        //SaveInventory.Clear();
        //SavePotionInventory.Clear();
        SaveInventoryItems.Clear();
        SavePotions.Clear();
        SaveQuickSlotPotions.Clear();

        SaveQuest.Clear();
        SaveQuestID.Clear();
        SaveEnemyQuestProgress.Clear();
        SaveNatureQuestProgress.Clear();

        SavePlayerSO();
        SaveSceneData();
        SavePlayerCurrentStateData();
        SavePlayerPositionData();
        SavePlayerEquipData();
        SaveGlobalTimeData();
        SavePlayerInventoryData();
        SavePlayerQuestData();
    }

    public void SaveSceneData()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    public void SavePlayerSO()
    {
        //PlayerLoadJsonData playerLoadJsonData = new PlayerLoadJsonData();
        //playerLoadJsonData.PlayerSaveData = SaveData;

        //SaveData.SetPlayerLevel(_playerData.GetPlayerLevel());
        //SaveData.SetPlayerMaxHealth(_playerData.GetPlayerMaxHealth());
        //SaveData.SetPlayerMaxStamina(_playerData.GetPlayerMaxStamina());
        //SaveData.SetPlayerAttack(_playerData.GetPlayerAtk());
        //SaveData.SetPlayerDef(_playerData.GetPlayerDef());
        //SaveData.SetPlayerExp(_playerData.GetPlayerExp());
        //SaveData.SetPlayerGold(_playerData.GetPlayerGold());

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

    // TODO SaveDataManager 에서 받아온 내용 
    public void SavePlayerCurrentStateData()
    {
        // SaveHealth = 10;

        SaveHealth = _healthSystem.Health;
        SaveStamina = _staminaSystem.Stamina;
        SavePlayerLevel = _expSystem.PlayerLevel;
        SavePlayerExp = _expSystem.PlayerExp;
        SavePlayerGold = _playerData.PlayerData.PlayerGold;
    }

    public void SavePlayerPositionData()
    {
        SavePlayerPosition = new SerializableVector3(_player.transform.position);
        //SavePlayerPosition = _player.transform.position;
        //_playerSO = GameManager.Instance.Player.GetComponent<Player>().Data;

    }

    public void SavePlayerEquipData()
    {
        for (int i = 0; i < _equipmentDatas.EquipData.Length; i++)
        {
            SaveEquipLevel[i] = _equipmentDatas.EquipData[i].Level;
        }

    }

    // 날짜와 시간 가져오기
    public void SaveGlobalTimeData()
    {
        SaveDay = _globalTimeManager.Day;
        SaveHour = _globalTimeManager.Hour;
        SaveMinutes = _globalTimeManager.Minutes;
        SaveDayTime = _globalTimeManager.DayTime;
    }

    // 인벤토리 아이템정보, 수량 가져오기
    // 포션 인벤토리 아이템 정보, 수량 가져오기
    public void SavePlayerInventoryData()
    {
        // 인벤토리 아이템 가져오기
        //for(int i = 0; i < _inventory.Slots.Count; i++)
        //{
        //    SaveInventory[i] = _inventory.Slots[i];
        //}

        foreach (ItemSlot slot in _inventory.Slots)
        {
            //SaveInventory.Add(slot);

            SaveInventoryItems.Add(slot.Item.ItemID, slot.Quantity);
        }

        foreach (PotionInventorySlot potionSlot in _potionInventory.Potions)
        {
            //SavePotionInventory.Add(potionSlot);

            SavePotions.Add(potionSlot.PotionSO.ID, potionSlot.InitQuantity);
        }

        SaveQuickSlotPotions.Add(_potionInventory.HPPotionQuick.PotionSO.ID, _potionInventory.HPPotionQuick.Quantity);
        SaveQuickSlotPotions.Add(_potionInventory.SPPotionQuick.PotionSO.ID, _potionInventory.SPPotionQuick.Quantity);
        // 포션 인벤토리 아이템 가져오기

        //for (int i = 0; i < _potionInventory.Potions.Length; i++)
        //{
        //    SavePotionInventory[i] = _potionInventory.Potions[i];
        //}

    }

    // 퀘스트 정보 가져오기
    // 퀘스트 진행상황 ex) 뱀을 한마리잡고 저장 후 다시 접속시 한마리 잡은상태
    // 퀘스트 목록 questManager.AcceptQuestList 에 포함되어있는 내용 가져오기
    public void SavePlayerQuestData()
    {
        //SaveEnemyQuestProgress.Clear();
        //SaveNatureQuestProgress.Clear();
        //SaveQuest.Clear();

        foreach (Quest quest in _questManager.AcceptQuestList)
        {
            SaveQuestID.Add(quest.QuestNumber);
            SaveQuest.Add(quest.TargetID, quest.TargetQuantity);

            Debug.Log("1" + quest.QuestNumber);
            Debug.Log("2" + quest.TargetID);
            Debug.Log("3" + quest.TargetQuantity);
        }

        foreach (var quest in _questManager.EnemyQuantityDict)
        {
            SaveEnemyQuestProgress.Add(quest.Key, quest.Value);
            Debug.Log("4" + quest.Key);
            Debug.Log("5" + quest.Value);
        }

        foreach (var quest in _questManager.NatureQuantityDict)
        {
            SaveNatureQuestProgress.Add(quest.Key, quest.Value);
            Debug.Log("6" + quest.Key);
            Debug.Log("7" + quest.Value);
        }

    }
}


public class SaveDataManager : MonoBehaviour
{
    public SavePlayerData SaveplayerData = new SavePlayerData();

    private void Start()
    {
        SaveplayerData.SetData(GameManager.Instance.Player);
    }

    // 저장할 때 호출되는 메서드
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
