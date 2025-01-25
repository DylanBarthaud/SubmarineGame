using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCreatureScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private float time = 10f;
    void Update(){
        rb.velocity = (Vector3.forward * -1) * 2000 * Time.deltaTime;

        
        if(time <= 0){
            Destroy(gameObject);
        }
        time -= Time.deltaTime;
    }
}
