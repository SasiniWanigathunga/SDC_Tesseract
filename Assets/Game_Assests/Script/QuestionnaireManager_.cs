using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionnaireManager_ : MonoBehaviour
{
    public string reactAppUrl = "http://localhost:3000/";
    public string apiBaseUrl = "http://localhost:8080/"; // Base URL for the API
    public string getScoreEndpoint = "question/getScore"; // API endpoint for getting the score

    // Method to send the JWT token and redirect the user
    public void SendJwtTokenAndRedirect()
    {
        // Access the JWT token from the GlobalManager singleton
        string jwtToken = GlobalManager_.Instance.JwtToken;

        // Create a URL with the JWT token as a query parameter
        string url = $"{reactAppUrl}?token={jwtToken}";

        // Open the URL in the default web browser to redirect the user
        Application.OpenURL(url);
    }

    // Example method to call when you want to redirect the user
    public void OnButtonClick()
    {
        GetScore();
    }

    // Method to get the score from the API
    public void GetScore()
    {
        // Access the JWT token from the GlobalManager singleton
        string jwtToken = GlobalManager_.Instance.JwtToken;

        // Create the full URL for the API request
        string url = $"{apiBaseUrl}{getScoreEndpoint}";

        // Start the coroutine to make the GET request
        StartCoroutine(GetScoreCoroutine(url, jwtToken));
    }

    private IEnumerator GetScoreCoroutine(string url, string jwtToken)
    {
        // Create a UnityWebRequest with the GET method
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Add the JWT token to the request header with the correct header name
        request.SetRequestHeader("JWT-Token", jwtToken);

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parse the response data (assumes the API returns the score as an integer)
            int score;
            if (int.TryParse(request.downloadHandler.text, out score))
            {
                // Output the score to the console log
                Debug.Log($"Score: {score}");
                // Add your code here to handle the score if needed

                // Set the score in the GlobalManager
                GlobalManager_.Instance.SetScore(score);
            }
            else
            {
                // Handle the case where parsing the score fails (unexpected data format)
                Debug.LogError("Failed to parse score from the API response.");
                GlobalManager_.Instance.SetScore(score);
            }
        }
        else
        {

            // Redirect the user as they may not have attempted the quiz
            SendJwtTokenAndRedirect();
        }
    }
}