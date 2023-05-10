using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Components
    private Rigidbody2D Rigidbody2D; // Rigidbody2D to store the rigidbody of the main character
    private Animator Animator; // Animator to store the animator of the main character
    private CapsuleCollider2D CapsuleCollider; // CapsuleCollider2D to store the capsule collider of the main character
    private Transform playerTransform; // Transform to store the transform of the player

    // Variables
    private float horizontal; // Float to store the horizontal axis
    public float speed = 0.6f; // Float to store the speed of the main character
    public float currentHealth; // Float to store the current health of the main character
    public float attackRange = 1f; // Float to store the attack range of the main character
    public int damage = 1;

    // Booleans
    private bool isPlayerInRange = false; // Boolean to store whether the player is in range of the main character

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        // // Check if the player is within range 
        // float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        // if (distanceToPlayer <= attackRange)
        // {
        //     isPlayerInRange = true;
        // }
        // else
        // {
        //     isPlayerInRange = false;
        // }

        // // If the player is in range, move towards them
        // if (isPlayerInRange)
        // {
        //     Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        //     transform.Translate(directionToPlayer * speed * Time.deltaTime);
        // }

        // horizontal = Input.GetAxis("Horizontal"); // Get the horizontal axis from the input manager

        // // Check the horizontal axis and set the scale and the is moving boolean
        // if (horizontal > 0.0f)
        // {
        //     transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // }
        // else if (horizontal < 0.0f)
        // {
        //     transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        // }

        // // If horizontal its diferent than 0, set the running animation to true
        // Animator.SetBool("Running", horizontal != 0.0f);
    }

    // // Function to deal damage to the enemy
    // private void OnCollisionEnter(Collision collision)
    // {
    //     // If the enemy collides with the player, deal damage
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Enemy collided with player");
    //         // PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //         // if (playerHealth != null)
    //         // {
    //         //     playerHealth.TakeDamage(damage);
    //         // }
    //     }
    // }
}
