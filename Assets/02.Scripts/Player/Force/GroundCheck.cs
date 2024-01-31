using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _forwardOffset = 0.2f;
    [SerializeField] private float _upOffset = 0.01f;
    [SerializeField] private float _rayDownY = -0.5f; 
    [SerializeField] private LayerMask _groundLayMask;

    private Vector3 _rayDown;

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

        _rayDown = new Vector3(0, _rayDownY, 0);

        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * _forwardOffset) - (Vector3.up * -_upOffset) , _rayDown),
            new Ray(transform.position + (-transform.forward * _forwardOffset)- (Vector3.up * -_upOffset), _rayDown),
            new Ray(transform.position + (transform.right * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown),
            new Ray(transform.position + (-transform.right * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown),
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
        _rayDown = new Vector3(0, _rayDownY, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown);
        Gizmos.DrawRay(transform.position + (-transform.forward * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown);
        Gizmos.DrawRay(transform.position + (transform.right * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown);
        Gizmos.DrawRay(transform.position + (-transform.right * _forwardOffset) - (Vector3.up * -_upOffset), _rayDown);
    }

}
