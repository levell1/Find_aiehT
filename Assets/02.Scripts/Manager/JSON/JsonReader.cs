using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;
    private AESManager _aesManager;
    public SavePlayerData LoadedPlayerData { get; private set; }



    private void Awake()
    {
        _aesManager = GameManager.Instance.AESManager;

        PlayerSkillData skillData = LoadJson<PlayerSkillData>(JsonDataName.PlayerSkillData);
        PlayerSO.SetPlayerSkillData(skillData);

        PlayerLevelData playerLevelData = LoadJson<PlayerLevelData>(JsonDataName.PlayerLevelData);
        PlayerSO.SetPlayerLevelData(playerLevelData);

    }

    public void InitPlayerData()
    {
        PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>(JsonDataName.PlayerData);
        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
    }

    public void LoadPlayerData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, JsonDataName.SaveFile);
        LoadedPlayerData = LoadJson<SavePlayerData>(saveFilePath);
        PlayerSO.SetPlayerData(LoadedPlayerData.InitLoadPlayerData());
    }

    public T LoadJson<T>(string FilePath)
    {
        //StringBuilder jsonFilePathBuilder = new StringBuilder(FilePath);
        //jsonFilePathBuilder.Append(".json");
        //string jsonFilePath = jsonFilePathBuilder.ToString();

        TextAsset jsonFile = Resources.Load<TextAsset>(FilePath);
        //string jsonText = File.ReadAllText(jsonFilePath);
        string jsonText = jsonFile.text;
        string decryptedJson = _aesManager.AESDecrypt(jsonText);

        return JsonConvert.DeserializeObject<T>(decryptedJson);
    }

    public void SaveJson(object data, string filePath)
    {
        StringBuilder jsonFilePathBuilder = new StringBuilder(filePath);
        jsonFilePathBuilder.Append(".json");

        string jsonFilePath = Path.Combine(Application.persistentDataPath, jsonFilePathBuilder.ToString());
        Debug.Log(jsonFilePath);

        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        string encryptedJson = _aesManager.AESEncrypt(jsonData);

        File.WriteAllText(jsonFilePath, encryptedJson);
    }

    public bool CheckJsonFileExist()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, string.Format("{0}.json", JsonDataName.SaveFile));
        bool saveFileExists = File.Exists(saveFilePath);

        return saveFileExists;
    }
}
