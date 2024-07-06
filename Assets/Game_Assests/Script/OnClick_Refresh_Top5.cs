using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnClick_Refresh_Top5 : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;
    public PopulateScrollView populateScrollView;

    // Text fields for displaying leaderboard data
    public TMP_Text FirstPlaceUser;
    public TMP_Text FirstPlaceScore;
    public TMP_Text SecondPlaceUser;
    public TMP_Text SecondPlaceScore;
    public TMP_Text ThirdPlaceUser;
    public TMP_Text ThirdPlaceScore;
    public TMP_Text FourthPlaceUser;
    public TMP_Text FourthPlaceScore;
    public TMP_Text FifthPlaceUser;
    public TMP_Text FifthPlaceScore;

    public void FetchUserProfiles()
    {
        leaderboardManager.FetchUserProfiles();
        StartCoroutine(WaitForUserProfiles());
    }

    private IEnumerator WaitForUserProfiles()
    {
        yield return new WaitUntil(() => leaderboardManager.AreUserProfilesLoaded);
        DisplayTop5Leaderboard();
    }

    private void DisplayTop5Leaderboard()
    {
        List<UserView> userViews = GlobalManager_.Instance.UserViews;

        // Display the top 5 users in the leaderboard
        FirstPlaceUser.text = userViews[0].Username;
        FirstPlaceScore.text = userViews[0].Score.ToString();
        SecondPlaceUser.text = userViews[1].Username;
        SecondPlaceScore.text = userViews[1].Score.ToString();
        ThirdPlaceUser.text = userViews[2].Username;
        ThirdPlaceScore.text = userViews[2].Score.ToString();
        FourthPlaceUser.text = userViews[3].Username;
        FourthPlaceScore.text = userViews[3].Score.ToString();
        FifthPlaceUser.text = userViews[4].Username;
        FifthPlaceScore.text = userViews[4].Score.ToString();

        populateScrollView.ScrollView();
    }
}
