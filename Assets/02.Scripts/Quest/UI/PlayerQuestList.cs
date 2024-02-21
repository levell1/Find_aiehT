using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestList : MonoBehaviour
{
    public GameObject[] QuestImageList;
    public GameObject[] ProgressList;

    private void Start()
    {
        GameManager.Instance.GlobalTimeManager.OnInitQuest += Init;
        GameManager.Instance.QuestManager.OnQuestAccepted += PlayerAceeptQuestList;
    }

    private void Init()
    {
        foreach (GameObject questImage in QuestImageList)
        {
            questImage.SetActive(false);
        }
    }

    public void PlayerAceeptQuestList(List<Quest> acceptedQuests, int quantity)
    {

        int minCount = Mathf.Min(QuestImageList.Length, acceptedQuests.Count);
        // 조건이 두개의 수를 비교해서 작은 수를 가져옴
        for(int i = 0; i < minCount; i++)
        {
            QuestImageList[i].GetComponent<PlayerQuestUI>().UpdateQuestUI(acceptedQuests[i], quantity);
        }

        if(GameManager.Instance.GameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            LoadQuantities();
        }
        
    }

    private void LoadQuantities()
    {
        for (int i = 0; i < GameManager.Instance.QuestManager.LoadAcceptQuestQuantities.Count; i++)
        {
            TextMeshProUGUI progressText = ProgressList[i].GetComponentInChildren<TextMeshProUGUI>();
            Slider progressSlider = ProgressList[i].GetComponentInChildren<Slider>();

            int currentQuantity = GameManager.Instance.QuestManager.LoadProgressQuantities[i];
            int TargetQuantity = GameManager.Instance.QuestManager.LoadAcceptQuestQuantities[i];

            string valueText = string.Format($"{currentQuantity} / {TargetQuantity}");
            progressText.text = valueText;

            float progressPercentage = (float)currentQuantity /TargetQuantity;
            progressSlider.value = progressPercentage;
        }

    }

}
