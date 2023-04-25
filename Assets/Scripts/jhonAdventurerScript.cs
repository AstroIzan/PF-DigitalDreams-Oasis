using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jhonAdventurerScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            Jump();
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

}
