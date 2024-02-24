using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Collider _weaponCollider;

    private float _damage;

    private readonly List<Collider> _alreadyCollidedObjects = new List<Collider>();
    [SerializeField] private EquipmentDatas _equipmentDatas;


    private void OnEnable()
    {
        InitializeCollider();
        _weaponCollider.enabled = false;
    }

    private void InitializeCollider()
    {
        _alreadyCollidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _weaponCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if(other.TryGetComponent(out EnemyHealthSystem health))
        {
            health.TakeDamage(_damage);
        }
        if (other.TryGetComponent(out BossHealthSystem bosshealth))
        {
            bosshealth.TakeDamage(_damage);
        }

    }

    public void SetAttack(float damage)
    {
        this._damage = damage +_equipmentDatas.SumDmg;
    }

    public void EnableCollider()
    {
        if (_weaponCollider != null)
        {
            _weaponCollider.enabled = true;
        }
    }

    public void DisableCollider()
    {
        if (_weaponCollider != null)
        {
            _weaponCollider.enabled = false;
            InitializeCollider();
        }
    }

}
