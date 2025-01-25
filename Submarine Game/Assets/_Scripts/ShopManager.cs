using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform shopTransform; 

    public void SpawnShop(){
        GameManager.Instance.ClearContainer(shopTransform);
        print("spawn shop"); 
        GameObject[] artefacts = GameManager.Instance.GetArtefacts();
        List<Artefact_Base> unlockedArtefacts = new List<Artefact_Base>();

        for (int i = 0; i < artefacts.Length; i++){
            if (artefacts[i].GetComponent<Artefact_Base>().IsUnlocked()){
                unlockedArtefacts.Add(artefacts[i].GetComponent<Artefact_Base>());
            }
        }

        for (int i = 0; i < unlockedArtefacts.Count; i++){

            ButtonDataStruct buttonData = new ButtonDataStruct(){
                eventParent = unlockedArtefacts[i].GetISpawnsButtons(),
                text = unlockedArtefacts[i].GetArtefactName(),
                sprite = unlockedArtefacts[i].GetSprite()
            };

            GameManager.Instance.LoadButton(buttonData, shopTransform); 
        }
    }

    public void LoadScene(int sceneId) {  
        GameManager.Instance.LoadScene(sceneId);
    }
}
