using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private GameObject playerObj;
    private GameObject player; 

    [SerializeField] private GameObject[] artefacts;
    [SerializeField] private GameObject buttonObjPreset;

    private List<GameObject> activeArtefacts = new List<GameObject>();
    private int gold; 

    private void Awake(){
        if (instance == null) {
            instance = this;
        }
        else { Destroy(gameObject); }
        
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.buildIndex == 1 && this != null){
            player = Instantiate(playerObj, new Vector3(0,3,0), transform.rotation);
            for (int i = 0; i < activeArtefacts.Count; i++){
                GameObject artefact = Instantiate(activeArtefacts[i]);
                player.GetComponent<Player>().AddArtefact(artefact);
            }
        }
    }

    public void LoadScene(int sceneId) {
        if (sceneId == 1) {
            Cursor.visible = false;
        }
        else { Cursor.visible = true; }
        SceneManager.LoadScene(sceneId);
    }

    public void resetGame(){
        foreach (GameObject artefact in artefacts){
            artefact.GetComponent<Artefact_Base>().ClearProgress();
        }
    }

    public void UnlockArtefact(int artefactId, int progressAmount){
        foreach (GameObject artefact in artefacts){
            if (artefact.GetComponent<Artefact_Base>().GetArtefactId() == artefactId){
                print("unlocked artefact"); 
                artefact.GetComponent<Artefact_Base>().AddProgress(progressAmount); 
            }
        }
    }

    public void ActivateArtefact(int artefactId) {
        foreach (GameObject artefact in artefacts){
            if (artefact.GetComponent<Artefact_Base>().GetArtefactId() == artefactId){
                activeArtefacts.Add(artefact);
            }
        }
    }

    public void LoadButton(ButtonDataStruct data, Transform transform){
        GameObject buttonObj = Instantiate(buttonObjPreset, transform);

        Button button = buttonObj.GetComponent<Button>();
        Image image = buttonObj.GetComponent<Image>();

        if (data.eventParent != null)
        {
            button.onClick.AddListener(() => data.eventParent.OnButtonClick(buttonObj));
        }
        else { Debug.LogWarning(buttonObj + ": hasn't been set an event parent"); }

        if (data.text != null)
        {
            TextMeshProUGUI text = buttonObj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.text = data.text;
        }

        if (data.sprite != null)
        {
            image.sprite = data.sprite;
        }
    }

    public void ClearContainer(Transform container){
        foreach (Transform child in container){
            if (child.GetComponent<Button>() != null){
                Destroy(child.gameObject);
            }
        }
    }

    public GameObject GetPlayer(){
        return player;
    }

    public GameObject[] GetArtefacts(){
        return artefacts; 
    }

    public void AddGold(int amount){
        gold += amount;
    }
}
