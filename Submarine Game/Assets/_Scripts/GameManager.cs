using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; 
    public static GameManager Instance {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is null");   
            }
            return instance;
        }
    }

    [SerializeField] private GameObject player;

    private void Awake()
    {
        instance = this;
    }

    void Start(){   
        Cursor.visible = false;
    }

    public GameObject GetPlayer(){
        return player;
    }
}
