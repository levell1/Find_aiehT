using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashForceReceiver : MonoBehaviour
{
     private Player _player;
   
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolTime = 2f;

    private float _dashTime = 0f;
    public bool _isDash { get; private set; }

    //private Vector3 _dashStartPosition;
    //private Vector3 _dashTargetPosition;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if(_isDash)
        {
            _dashTime += Time.fixedDeltaTime;

            if(_dashTime >= _dashDuration )
            {
                _isDash = false;

                Invoke("Reset", _dashCoolTime);
            }

        }    
    }

    public void Dash(float dashForce)
    {
        if(_player.GroundCheck.IsGrounded())
        {
            _isDash = true;
            _dashTime = 0f;

            StartCoroutine(DashCoroutine(dashForce));

            Debug.Log(dashForce);
        }
       
    }

    IEnumerator DashCoroutine(float dashForce)
    {
        Vector3 dashDirection = transform.forward;
        Vector3 dashPower = dashDirection;

       while (_dashTime <= _dashDuration)
        {
            
            dashPower += dashDirection * dashForce;

            //* -Mathf.Log(1 / Player.Rigidbody.drag)

            //_player.Rigidbody.velocity += dashPower;

            _player.Rigidbody.AddForce(dashPower, ForceMode.VelocityChange);

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    public void Reset()
    {
        _player.Rigidbody.velocity = Vector3.zero;
    }

}
