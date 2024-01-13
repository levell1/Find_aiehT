using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForceReceiver : MonoBehaviour
{
    [SerializeField] public Player Player;
    [SerializeField] private float _drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 _impact;

    void Update()
    {

        if (Player.Rigidbody.velocity.y < 0f && Player.GroundCheck.IsGrounded())
        {
            _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref dampingVelocity, _drag);

        }
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