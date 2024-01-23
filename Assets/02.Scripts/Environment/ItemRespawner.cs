using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> ItemWaitSpawnList = new List<GameObject>();
    public int CoCount;
    public float RespawnTime; // TODO 아침까지 남은시간 구하기

    //private float _remainigTime; // 다른 씬에서 소비한 시간 뺴기
    private Coroutine respawnCoroutine;

    private void FixedUpdate()
    {
        if (CoCount == 1)
        {
            respawnCoroutine = StartCoroutine(RespawnItem());
        }
    }

    private IEnumerator RespawnItem() //어쩌다보니 유사Queue 풀링
    {
        --CoCount;
        if (ItemWaitSpawnList[0] != null)
        {
            yield return new WaitForSeconds(RespawnTime);
            ItemWaitSpawnList[0].gameObject.SetActive(true);
            ItemWaitSpawnList.Remove(ItemWaitSpawnList[0]);
        }
    }

    private void OnEnable() //게임을 시작했을때, 씬 으로 왔을 때
    {
        if (ItemWaitSpawnList.Count != 0)
        {
            for(int i = 0; i < ItemWaitSpawnList.Count; ++i)
            {
                ItemWaitSpawnList[0].gameObject.SetActive(true);
            }
            ItemWaitSpawnList.Clear();
        }
    }

    private void OnDisable() //씬 이동할 때 따라다니지 않는 다면...
    {
        if (respawnCoroutine != null)
        {
            StopCoroutine(respawnCoroutine);
        }
    }
}
