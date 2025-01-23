using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCreatureSounds : MonoBehaviour{
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip[] clips;

    float timer;

    private void Start(){
        timer = 20; 
    }

    private void Update(){
        if(timer <= 0){
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
            timer = Random.Range(10, 40);
        }
        timer -= Time.deltaTime;
    }
}
