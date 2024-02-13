using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestList : MonoBehaviour
{
    public GameObject[] QuestImageList;

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

    private void PlayerAceeptQuestList(List<Quest> acceptedQuests)
    {
        // 조건이 두개의 수를 비교해서 작은 수를 가져옴
        for(int i = 0; i < QuestImageList.Length && i < acceptedQuests.Count; i++)
        {
            QuestImageList[i].GetComponent<PlayerQuestUI>().UpdateQuestUI(acceptedQuests[i]);
        }


        //foreach (Quest quest in acceptedQuests)
        //{
        //    questData.Add(quest);

        //    Debug.Log(quest.GetQuestDescription());
        //    Debug.Log(quest.GetQuestTitle());
        //}
    }


}
