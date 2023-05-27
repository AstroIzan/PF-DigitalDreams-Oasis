using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Components
    private Animator animator; // Animator to store the animator of the enemyes
    private GameObject target; // GameObject to store the player

    // Variables
    public int routine;         // Integer to store the routine of the enemy
    public float crono;         // Float to store the time
    public int direction;       // Integer to store the direction of the enemy
    public float speed_walk;    // Float to store the speed of the enemy
    public float speed_run;     // Float to store the speed of the enemy
    public bool isAttacking;    // Boolean to store if the enemy is attacking

    public float vision_range;  // Float to store the range of the vision of the enemy
    public float attack_range;  // Float to store the range of the attack of the enemy
    public GameObject range;    // GameObject to store the range of the enemy
    public GameObject hit;   // GameObject to store the attack of the enemy

    void Start()
    {
        // Get the components
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update() {
        behavior();
    }

    public void behavior() {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > vision_range && !isAttacking) {
            Debug.Log("Wihout Vision");
            animator.SetBool("Running", false);
            crono += 1 * Time.deltaTime;
            if (crono >= 3) {
                Debug.Log("Change Routine");
                routine = Random.Range(0, 2);
                crono = 0;
            }

            switch (routine)
            {
                case 0:
                    Debug.Log("Routine 0");
                    animator.SetBool("Running", false);
                    break;
                case 1:
                    Debug.Log("Routine 1");
                    direction = Random.Range(0, 2);
                    routine++;
                    break;
                case 2:
                    Debug.Log("Routine 2");
                    switch (direction)
                    {
                        case 0:
                            Debug.Log("Direction 0");
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                        case 1:
                            Debug.Log("Direction 1");
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("Running", true);
                    break;
            }
        } else {
            Debug.Log("With Vision");  
            if (Mathf.Abs(transform.position.x - target.transform.position.x) < attack_range && !isAttacking) {
                animator.SetBool("Running", true);
                transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                animator.SetBool("Attacking", false);

                if (transform.position.x < target.transform.position.x) {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                } else {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            } 
            else 
            {
                if (!isAttacking) {
                    if (transform.position.x < target.transform.position.x) {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    } else {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    animator.SetBool("Running", false);
                }
            }
        }
    }

    public void finalAnimation() {
        animator.SetBool("Attacking", false);   
        isAttacking = false;
        range.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue() {
        hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse() {
        hit.GetComponent<BoxCollider2D>().enabled = false;
    }
}
