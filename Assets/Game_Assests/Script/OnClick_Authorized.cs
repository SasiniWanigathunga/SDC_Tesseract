using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_Authorized : MonoBehaviour
{
    public SceneChangeManager_ sceneChangeManager;
    public GameObject PopUpAuth;

    public void Authorized()
    {
        StartCoroutine(UserProfileFetcher.FetchUserProfile());
        StartCoroutine(WaitForUserProfile());
    }

    private IEnumerator WaitForUserProfile()
    {
        yield return new WaitUntil(() => GlobalManager_.Instance.Username != null && GlobalManager_.Instance.Username != "");
        SceneChange();
    }

    public void SceneChange()
    {
        PopUpAuth.SetActive(false);
        sceneChangeManager.ChangetoMainMenu();
    }
}
