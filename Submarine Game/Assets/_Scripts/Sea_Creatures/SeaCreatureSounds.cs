using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCreatureSounds : MonoBehaviour{
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip[] clips;

    [SerializeField] GameObject seaCreature; 

    float timer;

    private void Start(){
        timer = 5; 
    }

    private void Update(){
        if(timer <= 0){
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
            timer = Random.Range(30, 60);

            int x = Random.Range(0, 3);
            if (x > -1) {
                Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
                Vector3 newPos = new Vector3(playerPos.x + 20, 3, playerPos.z + 20);
                Instantiate(seaCreature, newPos, new Quaternion(transform.rotation.x, transform.rotation.y +90, transform.rotation.z, transform.rotation.w));            
            }
        }
        timer -= Time.deltaTime;
    }
}
