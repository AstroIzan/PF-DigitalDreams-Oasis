using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainCharacterScript : MonoBehaviour
{   
    // Components
    private Rigidbody2D Rigidbody2D; // Rigidbody2D to store the rigidbody of the main character
    private Animator Animator; // Animator to store the animator of the main character
    private CapsuleCollider2D CapsuleCollider; // CapsuleCollider2D to store the capsule collider of the main character
    private GameObject punchMode; // GameObject to store the punch mode
    private GameObject swordMode; // GameObject to store the sword mode
    private CanvasGroup punchCanvas; // CanvasGroup to store the canvas of the punch mode
    private CanvasGroup swordCanvas; // CanvasGroup to store the canvas of the sword mode
    
    // Basic Movement Variables
    private float horizontal; // Float to store the horizontal axis
    public float speed = 1; // Float to store the speed of the main character
    private float jumpForce = 150; // Float to store the jump force of the main character
    public float rayCastSize = 0.2f; // Float to store the size of the ray cast
    public float doubleTapSpeedMultiplier = 1.3f; // Float to store the double tap speed multiplier
    public float currentSpeed; // Float to store the current speed of the main character

    // Advanced Movement Variables  
    private bool isGrounded; // Boolean to store if the main character is grounded
    private bool jumpPressed; // Boolean to store if the jump button is pressed
    private bool hasJumped; // Boolean to store if the main character has jumped
    private bool isMoving; // Boolean to store if the main character is moving
    private bool isCrouching; // Boolean to store if the main character is crouching
    public bool swordOn; // Boolean to store if the sword is out or not
    public bool isSprinting = false; // Boolean to store if the player has double tapped the sprint key
    private float lastTapTime = 0f; // Float to store the last tap time
    private float doubleTapTime = 0.2f; // Float to store the double tap time
    public float staminaCrouchRegeneration = 0.005f; // Float to store the stamina crouch regeneration
    public float staminaCurrentRegeneration = 0.003f; // Float to store the stamina current regeneration
    public float staminaSprintConsumption = 0.003f; // Float to store the stamina sprint depletion

    // Max Values Variables
    /* ALREADY NOT USED */ private float maxHealth = 10; // Float to store the max health of the main character
    /* ALREADY NOT USED */ private float maxcurrentStamina = 10; // Float to store the max currentStamina of the main character
    /* ALREADY NOT USED */ private float maxMana = 10; // Float to store the max mana of the main character

    // Current Values Variables [It has to be public to be accessed by the dataGameController script]
    public float currentHealth; // Float to store the current health of the main character
    public float currentStamina; // Float to store the currentStamina of the main character
    public float currentMana; // Float to store the mana of the main character
    public float currentMoney; // Float to store the money of the main character

    // Debug Variables To Be Removed After Debugging
    private float damage = 1; // Float to store the damage of the main character

    /**
     * Method to initialize the script
     * - Get the rigidbody
     * - Get the animator
     * - Get the capsule collider
     */
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();

        punchMode = GameObject.Find("Punch");
        swordMode = GameObject.Find("Sword");
        
        punchCanvas = punchMode.GetComponent<CanvasGroup>();
        swordCanvas = swordMode.GetComponent<CanvasGroup>();
    }

    /**
     * Method to update the script every frame
     */
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); // Get the horizontal axis from the input manager

        // Check the horizontal axis and set the scale and the is moving boolean
        if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            isMoving = true;
        }
        else if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // If horizontal its diferent than 0, set the running animation to true
        Animator.SetBool("Running", horizontal != 0.0f);

        // Check if the player has pressed the jump button
        jumpPressed = Input.GetKeyDown(KeyCode.W);

        // Check if the player is grounded and set the animation bool
        setJumpStatus(Physics2D.Raycast(transform.position, Vector2.down, rayCastSize));

        // Set the player crouch status and animation
        if (isGrounded) { crouch(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)); }

        
        // In case the player has pressed the movement arrows twice, then the sprint will be true
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            float timeSinceLastTap = Time.time - lastTapTime;
            isSprinting = false;
            if (timeSinceLastTap < doubleTapTime) { isSprinting = true; }
            lastTapTime = Time.time;
        }
        
        // In case the player is not pressing the movement arrows or the stamina is 0, then the sprint will be false
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || currentStamina <= 0 ) { isSprinting = false; }

        // Set the player stamina consumption and regeneration
        sprintStaminaStatus(isSprinting); 

        // If the jump button is pressed and the player is grounded, then the player will jump
        if (jumpPressed && Physics2D.Raycast(transform.position, Vector3.down, rayCastSize) && !hasJumped) { Jump(); }

        // When the player press Q or right click, the sword will be shown
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1)) { setSwordStatus(); }

        swordStatus(); // Set the sword status

        // Debug Methods
        if (Input.GetKeyDown(KeyCode.K)) { currentMoney += 1; }
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            TakeDamage(damage);
            currentMana -= damage;
            currentStamina -= damage;
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            // All full
            currentHealth = maxHealth;
            currentStamina = maxcurrentStamina;
            currentMana = maxMana;
        }

        // if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        // {
        //      Attack();
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
        if (isSprinting) { currentSpeed *= doubleTapSpeedMultiplier; }

        Rigidbody2D.velocity = new Vector2(horizontal * currentSpeed, Rigidbody2D.velocity.y);
    }

    /**
     * Method for the player can take damage
     * - Make a damage to the player
         * [IF the current health is less than 0] - Call the die method
     */
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /**
     * Method to make the player die
     * - Set the die animation to true
     * - Return to the main menu
     */
    private void Die()
    {
        currentHealth -= damage;

        // Animator.SetBool("Die", true);

        // return to main menu
        Debug.Log("You died!");
        currentHealth = 10;
    }

    /**
     * Method to check if the player has collided with something and set the animation status
     * - Check if the player has collided with something
         * [IF has collided with something] - Set the is grounded to true and set the jump animation to false
         * [ELSE] - Set the is grounded to false
     */
    private void setJumpStatus(RaycastHit2D raycastHit2D) {
        if (raycastHit2D.collider != null)
        {
            isGrounded = true;
            Animator.SetBool("Jumping", false);
            hasJumped = false;
        }
        else
        {
            hasJumped = true;
            isGrounded = false;
        }
    }

    /**
     * Method to check if the player is crouching
     * - Check if the player is crouching
         * [IF is crouching] - Set the horizontal to 0 and set the jump pressed to false
     */
    private void crouch(bool isCrouching)
    {
        Animator.SetBool("Crouching", isCrouching);
        if (isCrouching)
        {
            horizontal = 0f;
            jumpPressed = false;
            isSprinting = false;
            CapsuleCollider.size = new Vector2(0.1f, 0.15f);
            if (currentStamina < 10) { currentStamina += staminaCrouchRegeneration; }
        } 
        else
        {
            CapsuleCollider.size = new Vector2(0.1f, 0.25f);
        }
    }

    /**
     * Method to check and set the player stamina status
     * - Check if the player is sprinting
         * [IF is sprinting and stamina its more than 0] - Set the current stamina to the sprint consumption
         * [ELSE IF is not sprinting and stamina is less than 10] - Set the current stamina to the current regeneration
     */
    private void sprintStaminaStatus(bool isSprinting)
    {
        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= staminaSprintConsumption;
        }
        else if (!isSprinting && currentStamina < 10)
        {
            currentStamina += staminaCurrentRegeneration;
        }
    }

    /**
     * Method to show the sword
     * - Check if the player is not crouching, is not moving and is grounded
         * [IF is not crouching, is not moving, is grounded and don't get the sword out] - Set the sword on to true and set the show sword animation to true
         * [ELSE IF is not crouching, is not moving, is grounded and get the sword out] - Set the sword on to false and set the show sword animation to false
     */
    public void setSwordStatus()
    {
        if (!swordOn && !isCrouching && !isMoving && isGrounded)
        {
            swordOn = true;
        }
        else if (swordOn && !isCrouching && !isMoving && isGrounded)
        {
            swordOn = false;
        }
        showSword(swordOn);
    }

    public void showSword(bool swordOn) {
        Animator.SetBool("ShowSword", swordOn);
    }

    private void swordStatus() {
        if (swordOn) {
            swordCanvas.alpha = 1;
            punchCanvas.alpha = 0;
        } else {
            swordCanvas.alpha = 0;
            punchCanvas.alpha = 1;
        }
    }
}