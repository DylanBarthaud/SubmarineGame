using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float airTime; 
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;

    private int moveDirect;
    private float speed;
    private Animator animator;

    public float airLeft = 0; 

    private void Start(){
        airLeft = airTime; 
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update(){
        if (Input.GetKey(KeyCode.W)){
            if (moveDirect == -1) { speed = -speed; }
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

        else{
            speed = Mathf.Lerp(speed, 0, 5 * Time.deltaTime);
            animator.SetBool("playerIsMoving", false);
            if (speed < 0.01f){
                speed = 0;
            }
        }

        if (speed > maxSpeed){
            speed = maxSpeed;
        }

        if(airLeft <= 0){
            Destroy(gameObject);
        }
        airLeft -= Time.deltaTime;
    }

    private void FixedUpdate(){
        rb.velocity = (transform.forward * moveDirect) * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(cam.forward);
    }

    public void addAirTime(int time){
        airLeft += time; 
        if(airLeft > airTime){
            airLeft = airTime;
        }
    }

    public float GetAirTime() {
        return airLeft;
    }

    private void OnDestroy(){
        GameManager.Instance.LoadScene(0); 
    }
}
