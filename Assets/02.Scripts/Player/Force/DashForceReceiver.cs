using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashForceReceiver : MonoBehaviour
{
     private Player _player;
   
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolTime = 5f;

    private float _dashTime = 0f;
    public bool _isDash { get; private set; }
    private bool _isCollTime;

    //private Vector3 _dashStartPosition;
    //private Vector3 _dashTargetPosition;

    private void Start()
    {
        _player = GetComponent<Player>();
        _isCollTime = false;
        _isDash = false;
    }

    void FixedUpdate()
    {
        if(_isDash)
        {
            _dashTime += Time.fixedDeltaTime;

            if(_dashTime >= _dashDuration )
            {
                _isDash = false;

                if(!_isCollTime ) 
                    StartCoroutine(CollDown());
                
            }

        }    
    }

    public void Dash(float dashForce)
    {
        if (_player.GroundCheck.IsGrounded() && !_isDash)
        {
            _isDash = true;
            _dashTime = 0f;

            StartCoroutine(DashCoroutine(dashForce));

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

    IEnumerator CollDown()
    {
        _isCollTime = true;
        yield return new WaitForSeconds(_dashCoolTime);
        _isCollTime = false;
    }

}
