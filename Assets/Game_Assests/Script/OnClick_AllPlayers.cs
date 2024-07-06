using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_AllPlayers : MonoBehaviour
{
    public GameObject leaderboard_canvas;
    public PopulateScrollView populateScrollView;

    public void DisplayAllPlayers()
    {
        leaderboard_canvas.SetActive(true);
        populateScrollView.ScrollView();
    }
}
