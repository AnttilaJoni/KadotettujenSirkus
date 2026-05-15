using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    public float moveSpeed = 5f;
    public Vector3 moveDirection;

    private Vector2 input;

    [SerializeField ]Animator anim;
    private Vector2 lastMoveDirection;
    
    
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Inputs();
        Animate();
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(horizontal, vertical,  0);
    }
    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    private void FixedUpdate()
    {
        _rb2d.MovePosition(transform.position + moveDirection.normalized * (moveSpeed / 50) );
    }
}
