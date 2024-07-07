using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject object_Drag;
    public GameObject object_Game;
    public GameObject InstructionObject;
    // Start is called before the first frame update
    public Canvas canvas;
    private GameObject objectDragInstance;
    private GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    void Start()
    {
        gameManager = GameManager.instance;
        InstructionObject.SetActive(true);
        int lawnMowers = Mathf.FloorToInt(GlobalManager_.Instance.Score / 2);
        GlobalManager_.Instance.SetUpdateScore(lawnMowers);
        if (GlobalManager_.Instance.UpdateScore == 0)
        {
            GlobalVariable.Instance.SetElapsedTime(Time.time);
            InstructionObject.SetActive(false);
        }
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

        if (cardVal == 2 && GlobalManager_.Instance.UpdateScore > 0)                  // if the global variable updates correctly change this to 0
        {
            Debug.Log("Update Score: " + GlobalManager_.Instance.UpdateScore);
            gameManager.PlaceObject();
            gameManager.draggingObject = null;
            Destroy(objectDragInstance);
            GlobalManager_.Instance.SetUpdateScore(GlobalManager_.Instance.UpdateScore - 1);
            if (GlobalManager_.Instance.UpdateScore == 0)                              // if the global variable updates correctly change this to 0
            {
                GlobalVariable.Instance.SetElapsedTime(Time.time);
                InstructionObject.SetActive(false);
            }
        }
        else if (GlobalManager_.Instance.CreditCount >= cardVal && cardVal != 2)
        {
            Debug.Log("Placed");
            audioManager.PlayPlant(audioManager.plant);
            gameManager.PlaceObject();
            GlobalManager_.Instance.SetCreditConsumption(GlobalManager_.Instance.CreditCosumption + cardVal);
            gameManager.draggingObject = null;
            Destroy(objectDragInstance);
        }
        else
        {
            Destroy(objectDragInstance);
        }

    }
}