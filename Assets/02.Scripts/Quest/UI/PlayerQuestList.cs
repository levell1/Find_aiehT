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

    public void PlayerAceeptQuestList(Quest acceptedQuests, int quantity, int questNumber, int Index)
    {

        //int minCount = Mathf.Min(QuestImageList.Length, acceptedQuests.Count);
        //for (int i = 0; i < minCount; i++)
        //{
        QuestImageList[Index].GetComponent<PlayerQuestUI>().UpdateQuestUI(acceptedQuests, quantity);
        //}

        //if (GameManager.Instance.GameStateManager.CurrentGameState == GameState.LOADGAME)
        //{
        //    LoadQuantities(questNumber);
        //}
        Index += 1;
    }

    //private void LoadQuantities(int questNumber)
    //{
        //TextMeshProUGUI progressText = ProgressList[_listIndex].GetComponentInChildren<TextMeshProUGUI>();
        //Slider progressSlider = ProgressList[_listIndex].GetComponentInChildren<Slider>();

        //int currentQuantity = GameManager.Instance.QuestManager.LoadProgressQuantities[questNumber];
        //int TargetQuantity = GameManager.Instance.QuestManager.LoadAcceptQuestQuantities[_listIndex];

        //string valueText = string.Format($"{currentQuantity} / {TargetQuantity}");
        //progressText.text = valueText;

        //float progressPercentage = (float)currentQuantity / TargetQuantity;
        //progressSlider.value = progressPercentage;

    //}

}
