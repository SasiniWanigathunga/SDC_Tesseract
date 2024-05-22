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
        if (GlobalManager_.Instance.CreditCount >= cardVal)
        {
            Debug.Log("Dragging object cardValue: " + objectDragInstance.GetComponent<ObjectDragging>().cardValue);
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