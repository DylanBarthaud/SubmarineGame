using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed; 
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;

    private int moveDirect; 
    private float speed;
    private Animator animator;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update(){
        if (Input.GetKey(KeyCode.W)){
            if(moveDirect == -1) { speed = -speed; }
            moveDirect = 1;
            speed += accelerationSpeed * Time.deltaTime;
            animator.SetBool("playerIsMoving", true); 
        }

        else if (Input.GetKey(KeyCode.S)){
            if (moveDirect == 1) { speed = -speed; }
            moveDirect = -1;
            speed += accelerationSpeed * Time.deltaTime;
            animator.SetBool("playerIsMoving", true);
        } 

        else { 
            speed = Mathf.Lerp(speed, 0, 5 * Time.deltaTime);
            animator.SetBool("playerIsMoving", false);
            if (speed < 0.01f){
                speed = 0;
            }
        }

        if (speed > maxSpeed) { 
            speed = maxSpeed;
        }
    }

    private void FixedUpdate(){
        rb.velocity = (transform.forward * moveDirect) * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(cam.forward); 
    }
}
