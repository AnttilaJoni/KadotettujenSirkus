using System;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody _rb2d;

    public float moveSpeed = 5f;
    public Vector3 moveDirection;
    
    void Start()
    {
        _rb2d = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(horizontal, vertical,  0).normalized;
    }

    private void FixedUpdate()
    {
        _rb2d.MovePosition(transform.position + moveDirection * (moveSpeed / 20) );
    }
}
