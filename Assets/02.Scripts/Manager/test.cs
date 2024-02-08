using UnityEngine;

public class test : MonoBehaviour
{

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-5, 0, 0);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GameManager.Instance.Player.transform.position = new Vector3(5, 0, 8);
            LoadingSceneController.LoadScene(SceneName.TycoonScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-2, 0, 25);
            LoadingSceneController.LoadScene(SceneName.Field);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameManager.Instance.Player.transform.position = new Vector3(0, 0, 0);
            LoadingSceneController.LoadScene(SceneName.DungeonScene);
        }

    }
}
