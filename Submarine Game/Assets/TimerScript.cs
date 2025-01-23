using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Text text; 

    void Update(){
        GameObject player = GameManager.Instance.GetPlayer();
        Player playerScript = player.GetComponent<Player>();
        text.text = playerScript.GetAirTime().ToString();  
    }
}
