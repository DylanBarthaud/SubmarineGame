using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact_PickUp : MonoBehaviour
{
    [SerializeField] private float scanTime;
    [SerializeField] private int artefactId;
    [SerializeField] private int progressValue; 

    private void Update(){
        if (Vector3.Distance(GameManager.Instance.GetPlayer().transform.position, transform.position) < 10) { 
            scanTime -= Time.deltaTime;
        }
            
        if (scanTime <= 0){
            GameManager.Instance.UnlockArtefact(artefactId, progressValue); 
            Destroy(gameObject);
        }
    }

    public int GetArtefactId(){
        return artefactId;
    }
}
