using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEditor : EditorWindow
{
    [MenuItem("Window/Custom Developer/Scene")]
    public static void Open()
    {
        GetWindow<SceneEditor>("씬 이동");
    }


    private void OnGUI()
    {
        GUILayout.Label("씬 이동 버튼", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        if (GUILayout.Button("Vilage Scene"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-5, 0, 0);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Hunting Scene"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-2, 0, 25);
            LoadingSceneController.LoadScene(SceneName.Field);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Tycoon Scene"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-4, 0, 8);
            LoadingSceneController.LoadScene(SceneName.TycoonScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Tutorial Scene"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(0, 0, 0);
            LoadingSceneController.LoadScene(SceneName.TutorialScene);
        }

        GUILayout.EndVertical();

        GUILayout.Space(10);

        GUILayout.Label("마을 내 이동 버튼", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        if (GUILayout.Button("Vilage Front Tycoon"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(5, 0, -200);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Vilage Front Smith Shop"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-6, 0, -50);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Vilage Front Potion Shop"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(9, 0, -17);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Vilage Front Hunt Map"))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-4, 0, 27);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }


        GUILayout.EndVertical();

    }


}
