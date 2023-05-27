using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRange : MonoBehaviour
{
    public Animator animator; // Animator to store the animator of the enemyes
    public enemyScript enemyScript; // enemyScript to store the script of the enemyes

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", true);
            enemyScript.isAttacking = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void Start() {
        
    }
}
