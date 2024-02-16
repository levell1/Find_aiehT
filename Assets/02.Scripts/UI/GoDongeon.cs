using UnityEngine;

public class GoDongeon : BaseUI
{

    public void ClickButton() 
    {
        GameManager.Instance.Player.transform.position = new Vector3(0, 0, 0);
        LoadingSceneController.LoadScene(SceneName.DungeonScene);        // 골드 1000감소

    }

    
}
