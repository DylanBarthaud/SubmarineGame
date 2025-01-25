using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalVent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            Destroy(other.gameObject);
        }
    }

    private void Update(){
        float dist = Vector3.Distance(GameManager.Instance.GetPlayer().transform.position, transform.position);
        if (dist > 40){
            Destroy(gameObject);
        }
    }
}
