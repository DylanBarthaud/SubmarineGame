using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed; 
    [SerializeField] private Rigidbody rb;

    private int moveDirect; 
    public float speed; 

    private void Update(){
        if (Input.GetKey(KeyCode.W)){
            moveDirect = 1;
            speed += accelerationSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S)){
            moveDirect= -1;
            speed += accelerationSpeed * Time.deltaTime;
        } 

        else { 
            speed = Mathf.Lerp(speed, 0, 5 * Time.deltaTime);
            if (speed < 0.01f){
                speed = 0; 
            }
        }

        if (speed > maxSpeed) { 
            speed = maxSpeed;
        }
    }

    private void FixedUpdate(){
        rb.velocity = new Vector3(0, 0, speed * moveDirect * Time.deltaTime);
    }
}
