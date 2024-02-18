using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private DungeonManager _dungeonManager;
    private List<GameObject> _monsters = new List<GameObject>();
    private int _monsterCount = 0;
    [SerializeField] private Door[] _doors;

    private void Awake()
    {
        _dungeonManager = FindAnyObjectByType<DungeonManager>();
    }

    private void Start()
    {
        if (gameObject.name == "EndRoom"|| gameObject.name == "FirstRoom")
        {
            _monsterCount = 0;
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].DoorColliderActive();
            }
        }
        else
        {
            _monsterCount = Random.Range(4, 10);
            for (int i = 0; i < _monsterCount; i++)
            {
                int ran = Random.Range(0, 360);
                float x = Mathf.Cos(ran * Mathf.Deg2Rad) * 5f;
                float z = Mathf.Sin(ran * Mathf.Deg2Rad) * 5f;
                Vector3 pos = transform.position + new Vector3(x, 0, z);
                _monsters.Add(_dungeonManager.MonsterRandomSpawn(pos, transform));
                _monsters[i].GetComponent<EnemyHealthSystem>().OnDie += MonsterCheck;
            }
        }
    }
    private void MonsterCheck() 
    {
        _monsterCount --;
        if (_monsterCount<=0)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].DoorColliderActive();
            }
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _monsterCount; i++)
        {
            _monsters[i].GetComponent<EnemyHealthSystem>().OnDie -= MonsterCheck;
        }
    }
}
