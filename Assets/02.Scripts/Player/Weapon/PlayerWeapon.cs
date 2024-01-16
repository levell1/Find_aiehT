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
        _alreadyCollidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _weaponCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if(other.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_damage);
        }

    }

    public void SetAttack(int damage)
    {
        this._damage = damage;
    }

}
