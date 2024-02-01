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

    private void Start()
    {
        Player = GetComponent<Player>();
    }

    //private void Update()
    //{
    //    Debug.Log(Player.Rigidbody.velocity.y);
    //}

    //void Update()
    //{
    //    forceTime = Time.deltaTime;
    //    if (Player.Rigidbody.velocity.y <= 0f && Player.GroundCheck.IsGrounded())
    //    {
    //        _moveForce = Physics.gravity.y * forceTime;
    //    }

    //    _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    //    Player.Rigidbody.AddForce(_impact, ForceMode.Impulse);
    //}

    public void Jump(float jumpForce)
    {
        if(Player.GroundCheck.IsGrounded())
        {
            //Debug.Log(Player.Rigidbody.velocity.y);
            Player.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}