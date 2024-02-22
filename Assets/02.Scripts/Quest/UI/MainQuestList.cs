using System;
using System.Collections;
using System.Collections.Generic;
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
    private Quest[] _questList; // 매니저에서 가져온 퀘스트 리스트 저장
    private int _selectedQuestIndex = -1;
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

            // 토글이 변경될 때마다 해당 퀘스트의 인덱스를 전달하도록 수정
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
            _selectedQuestIndex = questIndex;
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
