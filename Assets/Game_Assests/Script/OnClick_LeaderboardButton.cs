using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_LeaderboardButton : MonoBehaviour
{
    public SceneChangeManager_ sceneChangeManager;
    public LeaderboardManager leaderboardManager;

    public void FetchUserProfiles()
    {
        leaderboardManager.FetchUserProfiles();
        StartCoroutine(WaitForUserProfiles());
    }

    private IEnumerator WaitForUserProfiles()
    {
        yield return new WaitUntil(() => GlobalManager_.Instance.UserViews != null && GlobalManager_.Instance.UserViews.Count > 0);
        SceneChangeToLeaderboard();
    }

    public void SceneChangeToLeaderboard()
    {
        sceneChangeManager.ChangetoLeaderboardScene();
    }
}
