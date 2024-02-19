using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TestingEditor : EditorWindow
{

    string _goldText;

    [MenuItem("Window/Custom Developer/Add Item Gold Window")]
    public static void OpenWindow()
    {
       GetWindow<TestingEditor>("골드 입력 및 아이템 추가");
    }

    private void OnGUI()
    {
        GUILayout.Label("골드 입력", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        _goldText = EditorGUILayout.TextField("Type Text", _goldText);

        if (GUILayout.Button("Add Gold"))
        {
            AddGold();
        }

        GUILayout.EndVertical();

        GUILayout.Space(10);

        GUILayout.Label("아이템 추가", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Item"))
        {
            AddItem();
        }

        

       
    }

    private void AddItem()
    {
        DataManager dataManager = FindObjectOfType<DataManager>();
       
        for (int i = 0; i < dataManager.FoodSODatas.Length; i++)
        {
            for (int j = 0; j < dataManager.FoodSODatas[i].Ingredients.Count; j++)
            {
                GameManager.Instance.Inventory.AddItem(dataManager.FoodSODatas[i].Ingredients[j].IngredientSO);
            }
        }
    }

    private void AddGold()
    {
        PlayerSO playerSO = FindObjectOfType<Player>().GetComponent<Player>().Data;

        if (playerSO != null && playerSO.PlayerData != null)
        {
            if (Int32.TryParse(_goldText, out int gold))
            {
                playerSO.PlayerData.PlayerGold = gold;
            }
        }
        else
        {
          
        }

    }


}
