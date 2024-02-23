using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public Toggle[] QuestToggle;
    public TMP_Text QuestContent;
    public TMP_Text QuestReward;
    public TMP_Text QuestRegion;

    public Image QuestStateImage;
    public Button AcceptButton;
    public TMP_Text AcceptButtonText;

    private QuestManager _questManager;
    private Quest[] _questList; 
    private int _selectedQuestIndex = -1; 

    private void Awake()
    {
        AcceptButtonText = AcceptButton.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Init();
        GameManager.Instance.GlobalTimeManager.OnInitQuest += Init;

        QuestToggle[0].isOn = true;
    }

    private void Init()
    {
        AcceptButton.interactable = true; 
        AcceptButtonText.text = "수락";

        AcceptButton.onClick.RemoveAllListeners();

        _questManager = GameManager.Instance.QuestManager;
        _questList = _questManager.ActiveDailyQuests.ToArray(); 

        for (int i = 0; i < _questList.Length; i++)
        {
            Quest quest = _questList[i];

            int questIndex = i;
            QuestToggle[i].GetComponentInChildren<Text>().text = quest.GetQuestTitle();
            QuestToggle[i].onValueChanged.AddListener((toggle) => { ToggleValueChanged(toggle, questIndex); });
        }
        AcceptButton.onClick.AddListener(AcceptQuest);
    }

    private void ToggleValueChanged(bool toggle, int questIndex)
    {
        QuestStateImage.gameObject.SetActive(false);
        AcceptButton.gameObject.SetActive(true);

        if (toggle)
        {
            _selectedQuestIndex = questIndex;
            QuestContent.text = _questList[questIndex].GetQuestDescription();
            QuestReward.text = _questList[questIndex].GetQuestRewardToString();
            QuestRegion.text = _questList[questIndex].GetQuestRegion();

            if (!_questManager.AcceptQuestList.Contains(_questList[questIndex]))
            {
                AcceptButton.interactable = true; 
                AcceptButtonText.text = "수락";
            }
            else
            {
                AcceptButton.interactable = false; 
                AcceptButtonText.text = "수락 완료";
            }


        }
    }

    private void AcceptQuest()
    {
        if (_selectedQuestIndex != -1)
        {
            Quest selectedQuest = _questList[_selectedQuestIndex];
            _questManager.AcceptQuest(selectedQuest); 
            
            AcceptButton.interactable = false; 
            AcceptButtonText.text = "수락 완료";
        }
    }
}
