using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    private static GameManager instance; 
    public static GameManager Instance {
        get{
            if (instance == null){
                Debug.LogError("GameManager is null");   
            }
            return instance;
        }
    }

    [SerializeField] private GameObject player;

    private void Awake(){
        instance = this;
    }

    public GameObject GetPlayer(){
        return player;
    }

    public void LoadScene(int sceneId) {
        if (sceneId == 0) { Cursor.visible = true; }
        else { Cursor.visible = false; }
        SceneManager.LoadScene(sceneId);
    }
}
