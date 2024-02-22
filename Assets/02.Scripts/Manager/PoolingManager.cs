using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolingManager : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public string ObjectName;
        public GameObject[] Prefabs;
        public int Size;
        public Transform SpawnPoint;
    }

    public List<Pool> Pools;
    Queue<GameObject> _poolObject;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize() //미리 생성 해둘 오브젝트
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            _poolObject = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                // TODO: 모든 오브젝트에 필요하지 않은 코드인데 써도 되나?
                int randomNum = UnityEngine.Random.Range(0, pool.Prefabs.Length);

                var newObj = Instantiate(pool.Prefabs[randomNum], pool.SpawnPoint);
                newObj.gameObject.SetActive(false);
                _poolObject.Enqueue(newObj);
            }
            poolDictionary.Add(pool.ObjectName, _poolObject);
        }
    }

    public void ReturnObject(GameObject obj) //오브젝트 비활성화 시킬 메서드
    {
        _poolObject.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetObject(string objname) // 호출
    {
        if (poolDictionary.ContainsKey(objname))
        {
            _poolObject = poolDictionary[objname];
            GameObject obj = _poolObject.Dequeue();
            obj.SetActive(true);

            return obj;
        }
        else
        {
            Debug.LogError($"{objname} - key값 존재하지 않음");
            return null;    // TODO
        }
    }

    //추가 생산
}
