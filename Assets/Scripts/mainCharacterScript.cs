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
    private CapsuleCollider2D CapsuleCollider;
    
    public bool isGrounded;
    public bool jumpPressed;
    public bool hasJumped;
    private bool isRunning;
    private bool isCrouching;
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
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
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
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, rayCastSize);

        if (raycastHit2D.collider != null)
        {
            isGrounded = true;
            Animator.SetBool("Jumping", false);
        }
        else
        {
            isGrounded = false;
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
        if (isGrounded)
        {
            isCrouching = Input.GetKey(KeyCode.S);
            Animator.SetBool("Crouching", isCrouching);
        }


        // if isCrouching is true, then the player cannot move
        if (isCrouching)
        {
            horizontal = 0;
            jumpPressed = false;
            // make the player's collider shorter
            CapsuleCollider.size = new Vector2(0.1f, 0.15f);
        } 
        else
        {
            // make the player's collider taller
            CapsuleCollider.size = new Vector2(0.1f, 0.25f);
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            money += 1;
        }

        if (Input.GetKeyDown(KeyCode.L)) 
        {
            TakeDamage(damage);
            mana -= damage;
            stamina -= damage;
        }

        // if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        // {
        //      Attack();
        // }

        if (jumpPressed && Physics2D.Raycast(transform.position, Vector3.down, rayCastSize) && !hasJumped)
        {
            Jump();
            hasJumped = true;
        }

        // if (input.GetKeyDown(KeyCode.Q))
        // {
        //     Animator.SetBool("ShowSword", );
        // }
    }

    /**
     * Method to jump
     * - Add force to the player
     * - Set the jump animation to false
     */
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
        Animator.SetBool("Jumping", true);
        
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

    /**
     * Method to check if the player has collided with something
     * - Check if the player has collided with something
         * [IF has collided with something] - Set the has jumped to false
     */


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    
    private void Die()
    {
        health -= damage;

        // Animator.SetBool("Die", true);

        // return to main menu
        Debug.Log("You died!");
        health = 10;
    }

    // private void Attack()
    // {
    //     Animator.SetTrigger("Attack");
    //     isAttacking = true;

    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         enemy.GetComponent<enemyScript>().TakeDamage(swordDamage);
    //     }
    // }

    // public void EndAttack()
    // {
    //     isAttacking = false;
    // }
}