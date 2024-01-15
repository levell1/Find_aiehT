using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private Player Player;
    [SerializeField] private float _drag = 0.3f;

    private Vector3 _dampingVelocity;
    private Vector3 _impact;
    private float _moveForce;

    private float forceTime;

    public Vector3 Movement => _impact * (-_moveForce); // 수직속도에 기타 영향을 줄 수 있는 impact를 더함

    private void Start()
    {
        Player = GetComponent<Player>();
    }

    void Update()
    {
        forceTime = Time.deltaTime;
        if (Player.Rigidbody.velocity.y <= 0f && Player.GroundCheck.IsGrounded())
        {
            _moveForce = Physics.gravity.y * forceTime;
        }
       
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);

    }

    public void Reset()
    {
        _impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }

    public void Jump(float jumpForce)
    {
        if(Player.GroundCheck.IsGrounded())
        {
            Player.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}