using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnClick_Profile : MonoBehaviour
{
    public GameObject profilePanel;
    public LeaderboardManager leaderboardManager;
    public TMP_Text leaderboard_rank;

    public void FetchUserProfiles()
    {
        leaderboardManager.FetchUserProfiles();
        StartCoroutine(WaitForUserProfiles());
    }

    private IEnumerator WaitForUserProfiles()
    {
        yield return new WaitUntil(() => GlobalManager_.Instance.UserViews != null && GlobalManager_.Instance.UserViews.Count > 0);
        DisplayUserProfile();
    }

    public void DisplayUserProfile()
    {
        profilePanel.SetActive(true);
        leaderboard_rank.text = GlobalManager_.Instance.LeaderboardRank.ToString();
        Debug.Log("Leaderboard Rank: " + GlobalManager_.Instance.LeaderboardRank);
    }
}
