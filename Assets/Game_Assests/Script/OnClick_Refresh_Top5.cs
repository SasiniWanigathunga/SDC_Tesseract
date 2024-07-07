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
        string our_username = GlobalManager_.Instance.Username;
        Debug.Log("Our username: " + our_username);

        // reset the color of the text fields
        FirstPlaceUser.color = Color.white;
        FirstPlaceScore.color = Color.white;
        SecondPlaceUser.color = Color.white;
        SecondPlaceScore.color = Color.white;
        ThirdPlaceUser.color = Color.white;
        ThirdPlaceScore.color = Color.white;
        FourthPlaceUser.color = Color.white;
        FourthPlaceScore.color = Color.white;
        FifthPlaceUser.color = Color.white;
        FifthPlaceScore.color = Color.white;

        // Display the top 5 users in the leaderboard
        FirstPlaceUser.text = userViews[0].Username;
        FirstPlaceScore.text = userViews[0].Score.ToString();
        if (FirstPlaceUser.text == our_username)
        {
            // change color to rgb value of red without using Color.red
            FirstPlaceUser.color = Color.red;
            FirstPlaceScore.color = Color.red;
            Debug.Log("Our username is in first place");
        }

        SecondPlaceUser.text = userViews[1].Username;
        SecondPlaceScore.text = userViews[1].Score.ToString();
        if (SecondPlaceUser.text == our_username)
        {
            SecondPlaceUser.color = Color.red;
            SecondPlaceScore.color = Color.red;
            Debug.Log("Our username is in second place");
        }

        ThirdPlaceUser.text = userViews[2].Username;
        ThirdPlaceScore.text = userViews[2].Score.ToString();
        if (ThirdPlaceUser.text == our_username)
        {
            ThirdPlaceUser.color = Color.red;
            ThirdPlaceScore.color = Color.red;
            Debug.Log("Our username is in third place");
        }

        FourthPlaceUser.text = userViews[3].Username;
        FourthPlaceScore.text = userViews[3].Score.ToString();
        if (FourthPlaceUser.text == our_username)
        {
            FourthPlaceUser.color = Color.red;
            FourthPlaceScore.color = Color.red;
            Debug.Log("Our username is in fourth place");
        }

        FifthPlaceUser.text = userViews[4].Username;
        FifthPlaceScore.text = userViews[4].Score.ToString();
        if (FifthPlaceUser.text == our_username)
        {
            FifthPlaceUser.color = Color.red;
            FifthPlaceScore.color = Color.red;
            Debug.Log("Our username is in fifth place");
        }

        populateScrollView.ScrollView();
    }
}
