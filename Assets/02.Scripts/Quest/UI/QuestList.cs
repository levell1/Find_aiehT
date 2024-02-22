//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuestList : MonoBehaviour
//{
//    public Toggle[] QuestToggle;
//    public TMP_Text QuestContent;
//    public Button AcceptButton;

//    private QuestManager _questManager;

//    private void Start()
//    {
//        _questManager = GameManager.Instance.QuestManager;
//        List<Quest> questList = _questManager.ActiveQuests;

//        int _enemyNum = 1;
//        int _natureNum = 1;

//        for (int i = 0; i < questList.Count; i++)
//        {
//            Quest quest = questList[i];

//            int questNumber = i < questList.Count / 2 ? _enemyNum++ : _natureNum++;
//            QuestToggle[i].isOn = false;
//            QuestToggle[i].GetComponentInChildren<Text>().text = quest.GetQuestTitle(questNumber);
//            QuestToggle[i].onValueChanged.AddListener((toggle) => { ToggleValueChanged(toggle, quest, questNumber); });

//            //// 버튼 클릭 이벤트 등록
//            //AcceptButton.onClick.AddListener(() =>
//            //{
//            //    if (quest is EnemyDailyQuest)
//            //    {
//            //        EnemyHealthSystem.OnQuestTargetDie += _questManager.UpdateEnemyQuestProgress;
//            //        Debug.Log("퀘스트 수락 및 이벤트 등록");
//            //    }
//            //    else if (quest is NatureDailyQuest)
//            //    {
//            //        ItemObject.OnQuestTargetInteraction += _questManager.UpdateNatureQuestProgress;
//            //        Debug.Log("채집물 퀘스트 수락 및 이벤트 등록");
//            //    }
//            //});

//        }
//    }

//    // 토글이 활성화 되었을 때 
//    // TODO 콘텐츠에 내용을 추가
//    // TODO testNum 삭제
//    private void ToggleValueChanged(bool toggle, Quest quest, int testNum)
//    {
//        if (toggle)
//        {
//            QuestContent.text = quest.GetQuestDescription();
//            Debug.Log(quest.QuestData.targetID);

//            // 퀘스트에 따라 이벤트 구독 및 몬스터 ID 전달
//            if (quest is EnemyDailyQuest)
//            {
//                AcceptEnemyQuest();
//            }
//            else if (quest is NatureDailyQuest)
//            {
//                AcceptNatureQuest();
//            }
//        }
//    }

//    private void AcceptEnemyQuest()
//    {
//            AcceptButton.onClick.AddListener(() =>
//            {
//                EnemyHealthSystem.OnQuestTargetDie += _questManager.UpdateEnemyQuestProgress;
//                Debug.Log("퀘스트 수락 및 이벤트 등록");
//            });

//        Debug.Log("액션 추가됨");
//    }

//    private void AcceptNatureQuest()
//    {
//        AcceptButton.onClick.AddListener(() =>
//        {
//            ItemObject.OnQuestTargetInteraction += _questManager.UpdateNatureQuestProgress;
//            Debug.Log("채집물 퀘스트 수락 및 이벤트 등록");
//        });
//    }


//    //private void ToggleValueChaged(bool toggle, Quest quest, int testNum)
//    //{
//    //    if(toggle)
//    //    {
//    //        QuestContent.text = quest.GetQuestDescription();

//    //        AcceptButton.onClick.RemoveAllListeners();

//    //        if (quest is EnemyDailyQuest)
//    //        {
//    //            AcceptButton.onClick.AddListener(
//    //                () => {
//    //                    EnemyHealthSystem.OnQuestTargetDie += _questManager.UpdateEnemyQuestProgress; 
//    //                    Debug.Log($"{quest.GetQuestTitle(testNum)} 퀘스트 등록"); 
//    //                }
//    //                );

//    //        }
//    //        else if (quest is NatureDailyQuest) 
//    //        {
//    //            AcceptButton.onClick.AddListener(
//    //                () => {
//    //                    ItemObject.OnQuestTargetInteraction += _questManager.UpdateNatureQuestProgress;
//    //                    Debug.Log("채집물 등록");
//    //                }
//    //                );

//    //        }

//    //    }

//    //}
//}



using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    public Toggle[] QuestToggle;
    public TMP_Text QuestContent;
    public TMP_Text QuestReward;

    public Image QuestStateImage;
    public Button AcceptButton;
    public TMP_Text AcceptButtonText;

    private QuestManager _questManager;
    private Quest[] _questList; // 매니저에서 가져온 퀘스트 리스트 저장
    private int _selectedQuestIndex = -1; // 선택된 퀘스트의 인덱스 저장

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
        AcceptButton.interactable = true; // 버튼 활성화
        AcceptButtonText.text = "수락";

        AcceptButton.onClick.RemoveAllListeners();

        _questManager = GameManager.Instance.QuestManager;
        _questList = _questManager.ActiveDailyQuests.ToArray(); // 퀘스트 리스트 복사

        for (int i = 0; i < _questList.Length; i++)
        {
            Quest quest = _questList[i];

            // 토글이 변경될 때마다 해당 퀘스트의 인덱스를 전달하도록 수정
            int questIndex = i;
            QuestToggle[i].GetComponentInChildren<Text>().text = quest.GetQuestTitle();
            QuestToggle[i].onValueChanged.AddListener((toggle) => { ToggleValueChanged(toggle, questIndex); });
        }

        // 수락 버튼에 대한 클릭 이벤트 등록
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

            if (!_questManager.AcceptQuestList.Contains(_questList[questIndex]))
            {
                AcceptButton.interactable = true; // 버튼 비활성화
                AcceptButtonText.text = "수락";
            }
            else
            {
                AcceptButton.interactable = false; // 버튼 비활성화
                AcceptButtonText.text = "수락 완료";
            }


        }
    }

    private void AcceptQuest()
    {
        // 선택된 퀘스트가 있을 경우에만 수락 처리
        if (_selectedQuestIndex != -1)
        {
            Quest selectedQuest = _questList[_selectedQuestIndex];
            _questManager.AcceptQuest(selectedQuest); // 선택된 퀘스트를 QuestManager에 전달하여 처리
            
            AcceptButton.interactable = false; // 버튼 비활성화
            AcceptButtonText.text = "수락 완료";
        }
    }


    //private void AcceptQuest()
    //{
    //    // 선택된 퀘스트가 있을 경우에만 수락 처리
    //    if (_selectedQuestIndex != -1)
    //    {
    //        Quest selectedQuest = _questList[_selectedQuestIndex];
    //        if (!_questManager.AcceptQuestList.Contains(selectedQuest))
    //        {
    //            if (selectedQuest is EnemyDailyQuest)
    //            {
    //                EnemyHealthSystem.OnQuestTargetDie += _questManager.UpdateEnemyQuestProgress;
    //                _questManager.AcceptQuestList.Add(selectedQuest);
    //                Debug.Log("퀘스트 수락 및 이벤트 등록: " + selectedQuest.GetQuestTitle(_selectedQuestIndex));
    //            }
    //            else if (selectedQuest is NatureDailyQuest)
    //            {
    //                ItemObject.OnQuestTargetInteraction += _questManager.UpdateNatureQuestProgress;
    //                _questManager.AcceptQuestList.Add(selectedQuest);
    //                Debug.Log("채집물 퀘스트 수락 및 이벤트 등록: " + selectedQuest.GetQuestTitle(_selectedQuestIndex));
    //            }
    //        }
    //    }
    //}


    //private void AcceptQuest()
    //{
    //    foreach (Toggle toggle in QuestToggle)
    //    {
    //        if (toggle.isOn)
    //        {
    //            int questIndex = Array.IndexOf(QuestToggle, toggle);
    //            Quest selectedQuest = _questList[questIndex];

    //            if (selectedQuest is EnemyDailyQuest)
    //            {
    //                EnemyHealthSystem.OnQuestTargetDie += _questManager.UpdateEnemyQuestProgress;
    //                Debug.Log("퀘스트 수락 및 이벤트 등록: " + selectedQuest.GetQuestTitle(questIndex));
    //            }
    //            else if (selectedQuest is NatureDailyQuest)
    //            {
    //                ItemObject.OnQuestTargetInteraction += _questManager.UpdateNatureQuestProgress;
    //                Debug.Log("채집물 퀘스트 수락 및 이벤트 등록: " + selectedQuest.GetQuestTitle(questIndex));
    //            }
    //        }
    //    }
    //}
}
