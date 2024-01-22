using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _forwardOffset = 0.2f;
    [SerializeField] private float _upOffset = 0.01f;
    [SerializeField] private LayerMask _groundLayMask;

    void Update()
    {
        IsGrounded();
    }

    /// <summary>
    /// ground 체크 레이
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * _forwardOffset) - (Vector3.up * -_upOffset) , Vector3.down),
            new Ray(transform.position + (-transform.forward * _forwardOffset)- (Vector3.up * -_upOffset), Vector3.down),
            new Ray(transform.position + (transform.right * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down),
            new Ray(transform.position + (-transform.right * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, _groundLayMask))
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * _forwardOffset) - (Vector3.up * -_upOffset), Vector3.down);
    }

}
