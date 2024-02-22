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

    private void Initialize() 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            _poolObject = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                int randomNum = UnityEngine.Random.Range(0, pool.Prefabs.Length);

                var newObj = Instantiate(pool.Prefabs[randomNum], pool.SpawnPoint);
                newObj.gameObject.SetActive(false);
                _poolObject.Enqueue(newObj);
            }
            poolDictionary.Add(pool.ObjectName, _poolObject);
        }
    }

    public void ReturnObject(GameObject obj) 
    {
        _poolObject.Enqueue(obj);
        obj.SetActive(false);
    }

    public GameObject GetObject(string objname) 
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
            return null;    
        }
    }
}
