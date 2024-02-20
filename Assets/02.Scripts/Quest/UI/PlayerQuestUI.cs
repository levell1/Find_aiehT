//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.Properties;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerQuestUI : MonoBehaviour
//{
//    public TMP_Text QuestTitle;
//    public TMP_Text QuestContent;
//    public TMP_Text QuestProgress;
//    public Slider SliderQuestProgress;

//    private Quest _quest;

//    private void Start()
//    {
//        GameManager.Instance.QuestManager.OnQuestValueUpdate += UpdateQuantityValue;
//    }

//    public void UpdateQuestUI(Quest quest)
//    {
//        _quest = quest;
//        gameObject.SetActive(true);
//        QuestTitle.text = quest.GetQuestTitle();
//        QuestContent.text = quest.GetQuestDescription();
//        UpdateQuantityValue(0);
//    }

//    private void UpdateQuantityValue(int currentQuantity)
//    {
//        string valueText = string.Format($"{currentQuantity} / {_quest.TargetQuantity}");
//        QuestProgress.text = valueText;

//        float progressPercentage = (float)currentQuantity / _quest.TargetQuantity;
//        SliderQuestProgress.value = progressPercentage;

//        if(SliderQuestProgress.value >= 1)
//        {
//            gameObject.SetActive(false);
//        }

//    }

//}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestUI : MonoBehaviour
{
    public TMP_Text QuestTitle;
    public TMP_Text QuestContent;
    public TMP_Text QuestProgress;
    public Slider SliderQuestProgress;

    private Quest _quest;
    // 나머지 코드 생략

    private void Start()
    {
        GameManager.Instance.QuestManager.OnQuestValueUpdate += UpdateQuantityValue;
    }

    public void UpdateQuestUI(Quest quest, int quantity)
    {
        _quest = quest;
        gameObject.SetActive(true);
        QuestTitle.text = quest.GetQuestTitle();
        QuestContent.text = quest.GetQuestDescription();

        UpdateQuantityValue(quantity, quest.TargetID);

    }

    public void UpdateQuantityValue(int currentQuantity, int targetID)
    {
        if (targetID != _quest.TargetID)
            return;

        string valueText = string.Format($"{currentQuantity} / {_quest.TargetQuantity}");
        QuestProgress.text = valueText;

        float progressPercentage = (float)currentQuantity / _quest.TargetQuantity;
        SliderQuestProgress.value = progressPercentage;

        Debug.Log(currentQuantity);

        if (SliderQuestProgress.value >= 1)
        {
            gameObject.SetActive(false);
            GameManager.Instance.QuestManager.OnQuestValueUpdate -= UpdateQuantityValue;
        }

    }

}

