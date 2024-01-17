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
        //// JSON 파일 경로 설정
        //string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        //// JSON 파일에서 데이터 읽기
        //string jsonText = File.ReadAllText(jsonFilePath);

        //PlayerJsonData playerJsonData = JsonUtility.FromJson<PlayerJsonData>(jsonText);

        //PlayerSO.SetPlayerData(playerJsonData.PlayerData);

        // LoadJson<PlayerJsonData>("PlayerData");
        SaveJson();

        PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>("PlayerData");
        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
        
        PlayerJsonSKillData playerJsonSkillData = LoadJson<PlayerJsonSKillData>("PlayerSkillData");
        playerJsonSkillData.SetSkillData(playerJsonSkillData.PlayerSkillData);
    }

    public T LoadJson<T>(string FilePath)
    {
        // JSON 파일 경로 설정
        string jsonFilePath = "Assets/Resources/JSON/" + FilePath + ".json"; // 연산량이 커짐

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonFilePath);

        return JsonUtility.FromJson<T>(jsonText);

    }

    public void SaveJson()
    {
        List<SkillInfoData> skillInfoDatas = PlayerSO.SkillData.SkillInfoDatas;

        SkillInfoData[] array = skillInfoDatas.ToArray();

        Debug.Log(PlayerSO.SkillData.SkillInfoDatas.Count);

        string json = JsonUtility.ToJson(array);

        File.WriteAllText("Assets/Resources/JSON/DummyData.json", json);
    }

}
