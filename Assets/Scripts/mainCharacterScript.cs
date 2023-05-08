using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacterScript : MonoBehaviour
{   
    // Variables
    public float speed = 1;
    public float jumpForce;
    public float rayCastSize;
    public float doubleTapSpeedMultiplier = 1.3f;
    public float health = 10;
    public float damage = 1f;
    public float stamina = 10;
    public float mana = 10;
    public float money = 0000;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    
    public bool isGrounded;
    public bool jumpPressed;
    public bool hasJumped;
    private bool isRunning;
    private bool isDoubleTap = false;
    private float horizontal;
    private float lastTapTime = 0f;
    private float currentSpeed;
    private float doubleTapTime = 0.2f;

    /**
     * Method to initialize the script
     * - Get the rigidbody
     * - Get the animator
     */
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    /**
     * Method to update the script every frame
     * - Get the horizontal axis
     * - Check the horizontal axis
         * [IF is greater than 0] - Set the scale to 1
         * [ELSE IF is less than 0] - Set the scale to -1
     * - Check if the player is running
         * [IF is running] - Set the running animation to true
     * - Check if the player has pressed the jump button
         * [IF has pressed the jump button] - Set the jump animation to true
     * - Check if the player is grounded
         * [IF is grounded] - Set the is grounded to true
         * [ELSE] - Set the is grounded to false
     * - Check if the player has jumped
         * [IF has jumped] - Call the jump method
     * - Check if the player has pressed the left or right arrow 
         * [IF has pressed the left or right arrow] - Set the last tap time
     * - Check if the player has pressed the left or right arrow twice
         * [IF has pressed the left or right arrow twice] - Set the double tap to true
         * [ELSE] - Set the double tap to false
     * - Set the last tap time
     */
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
        
        if (Physics2D.Raycast(transform.position, Vector3.down, rayCastSize)) 
        { 
            isGrounded = true; 
        } 
        else 
        { 
            isGrounded = false; 
        }

        if (jumpPressed && Physics2D.Raycast(transform.position, Vector3.down, rayCastSize) && !hasJumped)
        {
            Jump();
            hasJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) )
        {
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap < doubleTapTime)
            {
                isDoubleTap = true;
            }
            else
            {
                isDoubleTap = false;
            }

            lastTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            money += 1;
        }

        if (Input.GetKeyDown(KeyCode.L)) 
        {
            health -= damage;
            mana -= damage;
            stamina -= damage;
        }
    }

    /**
     * Method to jump
     * - Add force to the player
     */
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
        
    }

    /**
     * Method to update the script every physics frame
     * - Set the current speed
     * - Check if the player has double tapped
         * [IF has double tapped] - Set the current speed to the double tap speed multiplier
     * - Set the velocity
     */
    void FixedUpdate()
    {
        currentSpeed = speed;

        if (isDoubleTap)
        {
            currentSpeed *= doubleTapSpeedMultiplier;
        }

        Rigidbody2D.velocity = new Vector2(horizontal * currentSpeed, Rigidbody2D.velocity.y);
    }
    
    /**
     * Method to check if the player has collided with something
     * - Check if the player has collided with something
         * [IF has collided with something] - Set the has jumped to false
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasJumped = false;
    }
}