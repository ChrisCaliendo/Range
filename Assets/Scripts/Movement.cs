using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    public float movementSpeed;
    private Vector2 movementInput;
    private float currentVelocity;
    public float Horizontal;
    public float Vertical;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        if (Horizontal == 0 && Vertical == 0)
        {
            rigidbody2D.velocity = new Vector2(0, 0);
            animator.SetFloat("Speed", 0);
            return;
        }
        animator.SetFloat("Speed", 1);
        movementInput = new Vector2(Horizontal, Vertical);
        rigidbody2D.velocity = movementInput * movementSpeed * Time.fixedDeltaTime;
        
    }

    private void Animate()
    {
        animator.SetFloat("MovementX", Horizontal);
        animator.SetFloat("MovementY", Vertical);
        //animator.SetFloat("Speed", rigidbody2D.velocity.magnitude);
    }
}

