using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class UserProfileFetcher : MonoBehaviour
{
    public static IEnumerator FetchUserProfile()
    {
        string url = "http://20.15.114.131:8080/api/user/profile/view";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("accept", "*/*");
        request.SetRequestHeader("Authorization", "Bearer " + GlobalManager_.Instance.JwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string profileJson = request.downloadHandler.text;
            Dictionary<string, object> profileData = JsonConvert.DeserializeObject<Dictionary<string, object>>(profileJson);
            string userJson = JsonConvert.SerializeObject(profileData["user"]);
            Dictionary<string, object> userData = JsonConvert.DeserializeObject<Dictionary<string, object>>(userJson);

            // Set the username in the global manager
            GlobalManager_.Instance.SetUsername(userData["username"].ToString());
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}
