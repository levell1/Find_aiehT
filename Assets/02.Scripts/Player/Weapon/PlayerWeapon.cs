using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Collider _weaponCollider;

    private int _damage;

    private List<Collider> _alreadyCollidedObjects = new List<Collider>();

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
        if (other == _weaponCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if(other.TryGetComponent(out EnemyHealthSystem health))
        {
            Debug.Log("sddad");
            health.TakeDamage(_damage);
        }

    }

    public void SetAttack(int damage)
    {
        this._damage = damage;
    }

    // 콜라이더를 활성화할 때 이 메서드 호출
    public void EnableCollider()
    {
        if (_weaponCollider != null)
        {
            _weaponCollider.enabled = true;
        }
    }

    // 콜라이더를 비활성화할 때 이 메서드 호출
    public void DisableCollider()
    {
        if (_weaponCollider != null)
        {
            _weaponCollider.enabled = false;
            InitializeCollider();
        }
    }

}
