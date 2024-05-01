using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_ : MonoBehaviour
{
    public GameObject PopUpLost;

    void Start()
    {
        PopUpLost.SetActive(false);
    }

    void Update()
    {
        if (GlobalVariable.Instance.isLost)
        {
            PopUpLost.SetActive(true);
        }
    }
}
