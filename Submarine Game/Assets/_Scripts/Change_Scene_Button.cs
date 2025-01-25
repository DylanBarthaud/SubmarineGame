using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Scene_Button : MonoBehaviour
{
    public void loadScene(int sceneId){
        GameManager.Instance.LoadScene(sceneId);
    }
}
