using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_AllPlayers : MonoBehaviour
{
    public GameObject leaderboard_canvas;
    public PopulateScrollView populateScrollView;

    public void DisplayAllPlayers()
    {
        Debug.Log("Displaying all players");
        leaderboard_canvas.SetActive(true);
        populateScrollView.ScrollView();
    }
}
