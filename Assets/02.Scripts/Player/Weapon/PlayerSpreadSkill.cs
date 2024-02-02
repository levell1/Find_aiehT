using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadSkill : MonoBehaviour
{
    [SerializeField] private MeshCollider _skillRangeCollider;

    private float _damage;
    private float _playTime = 0.6f;

    private readonly List<Collider> _alreadyCollidedObjects = new List<Collider>();

    private EquipmentDatas _equipmentDatas;

    private void Awake()
    {
        if (_equipmentDatas == null)
        {
            _equipmentDatas = GameObject.FindWithTag(TagName.Player).GetComponent<EquipmentDatas>();
        }
    }

    private void Start()
    {
        DisableCollider();
    }

    private void OnEnable()
    {
        InitializeCollider();
    }

    private void InitializeCollider()
    {
        _alreadyCollidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _skillRangeCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if (other.TryGetComponent(out EnemyHealthSystem health))
        {
            Debug.Log(_damage);
            health.TakeDamage(_damage);
        }

    }

    public void SetAttack(float damage)
    {
        this._damage = damage + _equipmentDatas.SumDmg * 2;
    }

    public void EnableCollider()
    {
        StartCoroutine(DelayCollider());
    }

    private IEnumerator DelayCollider()
    {

        yield return new WaitForSeconds(_playTime);

        if (_skillRangeCollider != null)
        {
            _skillRangeCollider.enabled = true;
        }

    }

    // 콜라이더를 활성화할 때 이 메서드 호출
    //public void EnableCollider()
    //{
    //    if (_skillRangeCollider != null)
    //    {
    //        _skillRangeCollider.enabled = true;
    //    }
    //}

    // 콜라이더를 비활성화할 때 이 메서드 호출
    public void DisableCollider()
    {
        if (_skillRangeCollider != null)
        {
            _skillRangeCollider.enabled = false;
            InitializeCollider();
        }
    }
}
