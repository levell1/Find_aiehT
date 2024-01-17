using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager Instance;
    [Serializable]
    public struct Pool
    {
        public string ObjectName;
        public GameObject Prefab;
        public int Size;
        public Transform SpawnPoint;
    }
    public List<Pool> Pools;
    Queue<GameObject> _poolObject = new Queue<GameObject>();
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    private void Start()
    {
        GetObject("1");
    }

    private void Initialize() //미리 생성 해둘 오브젝트
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            for (int i = 0; i < pool.Size; i++)
            {
                var newObj = Instantiate(pool.Prefab, pool.SpawnPoint);
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

    public void GetObject(string objname) // 호출
    {
        if (poolDictionary.ContainsKey(objname))
        {
            _poolObject = poolDictionary[objname];
            GameObject obj = _poolObject.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            //_poolObject = null;
        }
    }

    //추가 생산
}
