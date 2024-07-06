using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    private bool areuserprofilesloaded = false;

    // Fetch player profile from the server
    public void FetchUserProfiles()
    {
        StartCoroutine(FetchProfiles());
    }

    public bool AreUserProfilesLoaded
    {
        get { return areuserprofilesloaded; }
    }

    public class Users
    {
        public List<UserView> UserViews { get; set; }
    }

    IEnumerator FetchProfiles()
    {
        string url = "http://20.15.114.131:8080/api/user/profile/list";  // URL for profile view endpoint

        // GET Request
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("accept", "*/*");
            string jwtToken = GlobalManager_.Instance.JwtToken;
            Debug.Log("JWT Token: " + jwtToken);
            string authHeaderValue = "Bearer " + jwtToken;
            Debug.Log("Authorization: " + authHeaderValue);
            request.SetRequestHeader("Authorization", authHeaderValue);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
                Debug.Log("Type of request.downloadHandler.text: " + request.downloadHandler.text.GetType());
                string usersJson = request.downloadHandler.text;

                Users users = JsonConvert.DeserializeObject<Users>(usersJson);

                // Access the userViews list
                List<UserView> userViews = users.UserViews;

                Debug.Log("Number of users: " + userViews.Count);
                int i = 0;

                // Create a new Random object
                System.Random random = new System.Random();

                // Iterate over the userViews list and print each user's first name
                foreach (UserView user in userViews)
                {
                    int score = random.Next(1, 101); // Generate a random score between 1 and 100

                    // get ourscore from GlobalManager
                    int ourscore = GlobalManager_.Instance.LeaderboardScore;
                    
                    if (user.Username == "oversight_g15")
                    {
                        user.Score = ourscore;
                        Debug.Log("Oversight's score: " + user.Score);
                    }
                    else 
                    {
                        user.Score = score;
                    }

                    // Debug.Log("User " + i + ": " + user.Username + " - " + user.Score);
                    i++;
                }
                
                Debug.Log("UserViews: " + JsonConvert.SerializeObject(userViews));
                // Sort the userViews list by score in descending order
                userViews = userViews.OrderByDescending(user => user.Score).ToList();
                Debug.Log("UserViews: " + JsonConvert.SerializeObject(userViews));

                // create a global variable to store the userViews
                GlobalManager_.Instance.SetUserViews(userViews);

                areuserprofilesloaded = true;
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }
}

