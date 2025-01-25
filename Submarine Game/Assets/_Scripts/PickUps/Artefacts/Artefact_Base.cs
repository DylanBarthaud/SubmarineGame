using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Artefact_Base : MonoBehaviour, ISpawnsButtons
{
    [SerializeField] private Sprite sprite; 
    [SerializeField] private string artefactName;

    [SerializeField] private int artefactId;
    [SerializeField] private int maxProgress;
    [SerializeField] private int price;
    private int currentProgress = 0;

    private bool isUnlocked = false; 

    public void AddProgress(int progress) {
        currentProgress += progress;
        print(currentProgress); 
        if (currentProgress >= maxProgress){  
            isUnlocked = true;
        }
    }

    public int GetArtefactId(){
        return artefactId;
    }

    public ISpawnsButtons GetISpawnsButtons(){
        return this; 
    }

    public Sprite GetSprite(){  
        return sprite;
    }

    public string GetArtefactName() {  
        return artefactName;
    }

    public bool IsUnlocked() {  
        return isUnlocked;
    }

    public void OnButtonClick(GameObject buttonObj){
        print("Activated: " + artefactName); 
        GameManager.Instance.ActivateArtefact(artefactId);
        isUnlocked = false;
        GameObject.FindWithTag("Shop").GetComponent<ShopManager>().SpawnShop(); 
    }

    public void ClearProgress(){
        currentProgress = 0;
        isUnlocked = false;
    }
}
