using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    protected DailyQuestData _enemyQuestData;
    protected DailyQuestData _natureQuestData;
    protected MainQuestData[] _mainQuestDataList;

    public QuestSO QuestData;

    protected Player _player;
    protected GameStateManager _gameStateManager;
    protected int _minTargetID;
    protected int _maxTargetID;

    protected string _enemyName;
    protected string _natureItemName;

    protected Region _enemyRegion;
    protected Region _natureRegion;

    protected int _minTargetQuantity = 1;
    protected int _maxTargetQuantity;

    public int EnemyQuestReward;
    public int NatureItemPrice;
    public int MainQuestReward;

    public int NatureToalQuestReward;
    public int EnemyTotalQuestReward;

    public int QuestNumber;
    public int TargetID;
    public int TargetQuantity;
    public bool IsProgress;

    public ItemDataListSO ItemDatas = GameManager.Instance.DataManager.NatureItemDataList;
    public EnemyDataListSO EnemyDatas = GameManager.Instance.DataManager.EnemyDataList;

    protected List<int> _randomIDList =  new List<int>();
   
    public Quest(QuestSO data, int questNumber)
    {

        QuestData = data;
        _enemyQuestData = data.EnemyQuestData;
        _natureQuestData = data.NatureQuestData;


        QuestNumber = questNumber;
        EnemyQuestReward = data.EnemyQuestData.reward;
        _player = GameManager.Instance.Player.GetComponent<Player>();
        

        InitQuest();
    }

    protected virtual void InitQuest() {}

    protected virtual void RandomQuest()
    {
        _gameStateManager = GameManager.Instance.GameStateManager;

        if(_gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            TargetQuantity = Random.Range(_minTargetQuantity, _maxTargetQuantity);

            int index = Random.Range(0, _randomIDList.Count);

            TargetID = _randomIDList[index];
            _randomIDList.RemoveAt(index);

            foreach (var enemyData in EnemyDatas.EnemyList)
            {
                if (enemyData.EnemyID == TargetID)
                {
                    _enemyName = enemyData.EnemyName;
                    _enemyRegion = enemyData.EnemyRegion;
                    break;
                }
            }

            foreach (var natureItem in ItemDatas.ItemList)
            {
                if (natureItem.ItemID == TargetID)
                {
                    _natureItemName = natureItem.ObjName;
                    _natureRegion = natureItem.Region;
                    NatureItemPrice = natureItem.Price;
                    break;
                }
            }
        }
    }

    public virtual string GetQuestTitle()
    {
        return string.Format("사냥 퀘스트");
    }

    //TODO 로그 추후에 UI로 변경
    public virtual string GetQuestDescription()
    {
        return string.Format($"몬스터 ID {_enemyName}을/를 가진 몬스터를 {TargetQuantity}마리 잡아라");
    }

    public virtual string GetQuestRegion()
    {
        return string.Format($"출몰 지역: {_enemyRegion}");
    }

    public virtual int GetTargetID()
    {
        return TargetID;
    }

    public virtual string GetQuestRewardToString()
    {
        EnemyTotalQuestReward = EnemyQuestReward * _player.Data.PlayerData.PlayerLevel;
        return EnemyTotalQuestReward.ToString();
    }

}
