using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainQuestList : MonoBehaviour
{
    public Toggle[] QuestToggle;
    public TMP_Text QuestContent;
    public TMP_Text QuestReward;

    public Image QuestStateImage;
    public Button AcceptButton;
    private TMP_Text _questStateImageText;

    private QuestManager _questManager;
    private Quest[] _questList; 
    private void Awake()
    {
        _questStateImageText = QuestStateImage.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _questManager = GameManager.Instance.QuestManager;
        _questList = _questManager.ActiveMainQuests.ToArray();

        for (int i = 0; i < _questList.Length; i++)
        {
            Quest quest = _questList[i];

            int questIndex = i;
            QuestToggle[i].GetComponentInChildren<Text>().text = quest.GetQuestTitle();
            QuestToggle[i].onValueChanged.AddListener((toggle) => { ToggleValueChanged(toggle, questIndex); });
        }
    }

    private void ToggleValueChanged(bool toggle, int questIndex)
    {
        AcceptButton.gameObject.SetActive(false);
        QuestStateImage.gameObject.SetActive(true);

        if (toggle)
        {
            QuestContent.text = _questList[questIndex].GetQuestDescription();
            QuestReward.text = _questList[questIndex].GetQuestRewardToString();

            if (!_questList[questIndex].IsProgress)
            {
                QuestStateImage.color = new Color(0.3f, 0.69f, 1);
                _questStateImageText.color = new Color(0, 0, 0);
                _questStateImageText.text = string.Format("퀘스트 진행중");
            }
            else
            {
                QuestStateImage.color = new Color(0.7f, 0.67f, 0);
                _questStateImageText.color = new Color(1, 1, 1);
                _questStateImageText.text = string.Format("퀘스트 완료");
            }

        }
    }
}
