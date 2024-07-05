using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_LeaderboardButton : MonoBehaviour
{
    public SceneChangeManager_ sceneChangeManager;
    public LeaderboardManager leaderboardManager;

    public void OnClick()
    {
        leaderboardManager.FetchUserProfiles();
        sceneChangeManager.ChangetoLeaderboardScene();
    }
}
