using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacterScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    
    private float horizontal;
    private bool isGrounded;
    private bool jumpPressed;
    private bool hasJumped;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("Running", horizontal != 0.0f);

        jumpPressed = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
        
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.23f)) 
        { 
            isGrounded = true; 
        } 
        else 
        { 
            isGrounded = false; 
        }

        if (jumpPressed && isGrounded && !hasJumped)
        {
            Jump();
            hasJumped = true;
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasJumped = false;
    }

}