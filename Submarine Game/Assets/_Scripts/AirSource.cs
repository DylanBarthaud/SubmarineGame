using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSource : MonoBehaviour{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().addAirTime(5); 
            Destroy(gameObject);
        }
    }

    private void Update(){
        float dist = Vector3.Distance(GameManager.Instance.GetPlayer().transform.position, transform.position);
        if(dist > 40){
            Destroy(gameObject);
        }
    }
}
