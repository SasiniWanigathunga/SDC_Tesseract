using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    // Start is called before the first frame update
    public Canvas canvas;
    private GameObject objectDragInstance;
    private GameManager gameManager;
    

    void Start()
    {
        gameManager = GameManager.instance;
        GlobalManager_.Instance.SetUpdateScore(GlobalManager_.Instance.Score);
    }

    public void OnDrag(PointerEventData eventData)

    {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectDragInstance = Instantiate(object_Drag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
        objectDragInstance.GetComponent<ObjectDragging>().card = this;
        

        gameManager.draggingObject = objectDragInstance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int cardVal = objectDragInstance.GetComponent<ObjectDragging>().cardValue;

        if (cardVal == 2 && GlobalManager_.Instance.UpdateScore > -3)                  // if the global variable updates correctly change this to 0
        {
            Debug.Log("Update Score: " + GlobalManager_.Instance.UpdateScore);
            gameManager.PlaceObject();
            gameManager.draggingObject = null;
            Destroy(objectDragInstance);
            GlobalManager_.Instance.SetUpdateScore(GlobalManager_.Instance.UpdateScore - 1);
            if (GlobalManager_.Instance.UpdateScore == -2)                              // if the global variable updates correctly change this to 1
            {
                GlobalVariable.Instance.SetElapsedTime(Time.time);;
            }

            
        }        
        else if (GlobalManager_.Instance.CreditCount >= cardVal && cardVal != 2)
        {
            Debug.Log("Placed");
            gameManager.PlaceObject();        
            GlobalManager_.Instance.SetCreditConsumption(GlobalManager_.Instance.CreditCosumption+cardVal);
            gameManager.draggingObject = null;
            Destroy(objectDragInstance);
        }            
        else
        {
            Destroy(objectDragInstance);
        }

    }
}