using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;

    private void Awake()
    {
        //// JSON ���� ��� ����
        //string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        //// JSON ���Ͽ��� ������ �б�
        //string jsonText = File.ReadAllText(jsonFilePath);

        //PlayerJsonData playerJsonData = JsonUtility.FromJson<PlayerJsonData>(jsonText);

        //PlayerSO.SetPlayerData(playerJsonData.PlayerData);

        // LoadJson<PlayerJsonData>("PlayerData");

        PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>("PlayerData");
        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
        
        PlayerSkillData skillData = LoadJson<PlayerSkillData>("PlayerSkillData");
        PlayerSO.SetPlayerSkillData(skillData);
    
    }

    public T LoadJson<T>(string FilePath)
    {
        // JSON ���� ��� ����
        string jsonFilePath = "Assets/Resources/JSON/" + FilePath + ".json"; // ���귮�� Ŀ��

        // JSON ���Ͽ��� ������ �б�
        string jsonText = File.ReadAllText(jsonFilePath);

        return JsonUtility.FromJson<T>(jsonText);

    }

}
