using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Quest Data")]
public class QuestSO : ScriptableObject
{
    [field: SerializeField] public DailyQuestData EnemyQuestData { get; private set; }
    [field: SerializeField] public DailyQuestData NatureQuestData { get; private set; }
    [field: SerializeField] public MainQuestData[] MainQuestData { get; private set; }

}
