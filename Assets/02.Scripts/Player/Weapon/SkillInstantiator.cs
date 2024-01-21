using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SkillInstantiator : MonoBehaviour
{
    //[field: SerializeField] private GameObject SkillPos;
    //private GameObject _skillPrefabs;

    //[field: SerializeField] private float _instantiateTime = 0.5f;

    //private PlayerSO _playerSO;
    //private string _skillNames = "Tomato";

    //private void Start()
    //{
    //    _playerSO = GetComponent<Player>().Data;

    //    StringBuilder _skillResourcePath = new StringBuilder("Prefabs/Skills/");
    //    _skillResourcePath.Append(_skillNames);

    //    Debug.Log(_skillResourcePath);

    //    _skillPrefabs = GameManager.instance.ResourceManager.Load<GameObject>("Prefabs/Skills/Tomato");

    //}

    //public void InstantiateTomato()
    //{
    //    StartCoroutine(DelayInstantiate());
    //}

    //IEnumerator DelayInstantiate()
    //{
    //    yield return new WaitForSeconds(_instantiateTime);
    //    Instantiate(_skillPrefabs, SkillPos.transform.position, Quaternion.identity);
    //}

    [field: SerializeField] private GameObject SkillPos;
    private GameObject _skillPrefabs;

    private List<GameObject> _skillPrefabList;

    [field: SerializeField] private float _instantiateTime = 0.5f;

    private PlayerSO _playerSO;
    private List<string> _skillNames;

    private void Start()
    {
        _playerSO = GetComponent<Player>().Data;
        _skillNames = new List<string>();
        _skillPrefabList = new List<GameObject>();

        List<SkillInfoData> _skillData = _playerSO.SkillData.SkillInfoDatas;

        foreach (SkillInfoData skillInfoDatas in _skillData)
        {
            _skillNames.Add(skillInfoDatas.GetSkillPrefabsName());
        }

        foreach (string skillName in _skillNames)
        {

            StringBuilder _skillResourcePath = new StringBuilder("Prefabs/Skills/");
            _skillResourcePath.Append(skillName);

            _skillPrefabs = GameManager.instance.ResourceManager.Load<GameObject>(_skillResourcePath.ToString());

            _skillPrefabList.Add(_skillPrefabs);
        }

    }


    public void InstantiateTomato()
    {
        foreach (GameObject tomatoPrefab in _skillPrefabList.Where(Prefab => Prefab != null && Prefab.name == "Tomato"))
        {
            StartCoroutine(DelayInstantiate(tomatoPrefab));
        }

    }

    IEnumerator DelayInstantiate(GameObject tomatoPrefab)
    {
        yield return new WaitForSeconds(_instantiateTime); 
        Instantiate(tomatoPrefab, SkillPos.transform.position, Quaternion.identity);
    }

}
