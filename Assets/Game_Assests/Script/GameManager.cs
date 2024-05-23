using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject draggingObject;
    public GameObject currentContainer;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaceObject()
    {
        //int cardVal = draggingObject.GetComponent<ObjectDragging>().card.cardValue;
        int cardVal = draggingObject.GetComponent<ObjectDragging>().cardValue;

        if (draggingObject != null && currentContainer != null && cardVal == 2)
        {
            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.object_Game, currentContainer.transform);
            objectGame.GetComponent<LawnMowerController>().zombies =currentContainer.GetComponent<ObjectContainer>().spawnPoint.zombies;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;

            
        }
        else if (draggingObject != null && currentContainer != null)
        {
            GameObject objectGame = Instantiate(draggingObject.GetComponent<ObjectDragging>().card.object_Game, currentContainer.transform);
            objectGame.GetComponent<PlantController>().zombies =currentContainer.GetComponent<ObjectContainer>().spawnPoint.zombies;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
        

        
    }
}
