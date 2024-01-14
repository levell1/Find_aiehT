using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public int Speed;
    public float rotateSpeed = 10.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(h, 0, v).normalized;

        _rigidbody.velocity = move * Speed;

        //if (!(h == 0 && v == 0))
        //{
        //    _rigidbody.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotateSpeed);
        //}
    }
}
