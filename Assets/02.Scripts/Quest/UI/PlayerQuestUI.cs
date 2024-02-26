using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestUI : MonoBehaviour
{
    public TMP_Text QuestTitle;
    public TMP_Text QuestContent;
    public TMP_Text QuestProgress;
    public TMP_Text QuestRegion;
    public Slider SliderQuestProgress;

    private Quest _quest;

    private void OnEnable()
    {
        GameManager.Instance.QuestManager.OnQuestValueUpdate += UpdateQuantityValue;
    }

    public void UpdateQuestUI(Quest quest, int quantity)
    {
        _quest = quest;
        gameObject.SetActive(true);
        QuestTitle.text = quest.GetQuestTitle();
        QuestContent.text = quest.GetQuestDescription();
        QuestRegion.text = quest.GetQuestRegion();

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

        if (SliderQuestProgress.value >= 1)
        {
            gameObject.SetActive(false);
            GameManager.Instance.QuestManager.OnQuestValueUpdate -= UpdateQuantityValue;
        }

    }

}

