using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_AllPlayers : MonoBehaviour
{
    public GameObject leaderboard_canvas;
    public LeaderboardManager leaderboardManager;
    public PopulateScrollView populateScrollView;

    public void FetchUserProfiles()
    {
        leaderboardManager.FetchUserProfiles();
        StartCoroutine(WaitForUserProfiles());
    }

    private IEnumerator WaitForUserProfiles()
    {
        yield return new WaitUntil(() => GlobalManager_.Instance.UserViews != null && GlobalManager_.Instance.UserViews.Count > 0);
        ActivateCanvas();
    }

    public void ActivateCanvas()
    {
        leaderboard_canvas.SetActive(true);
        populateScrollView.ScrollView();
    }
}
