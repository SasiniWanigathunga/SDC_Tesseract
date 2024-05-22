using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager_ : MonoBehaviour
{
    public Button profileButton;  // Reference to the profile button in the UI
    public GameObject canvas2;
    public ProfileManager_ profileManager;  // Reference to the ProfileManager script

    void Start()
    {
        profileButton.onClick.AddListener(PopUpCanvas);
    }
    
    void PopUpCanvas()
    {
        canvas2.SetActive(true);
        profileManager.FetchProfile();
    }
}
