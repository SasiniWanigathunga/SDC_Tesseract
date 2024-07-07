using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AuthenticationManager_ : MonoBehaviour
{
    private string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI0OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhYQ";  // API key for authentication
    public Button authorizeButton; // Reference to the Login button
    public GameObject PopUpAuth;  //Pop Up panel if Authentication is successful
    public GameObject PopUpAuthError;  //Pop Up panel if Authentication is unsuccessful

    void Start()
    {
        GameObject.Find("AuthorizeButton").GetComponent<Button>().onClick.AddListener(Authorize);

        // Hide the authentication pop-up panels initially
        PopUpAuth.SetActive(false);
        PopUpAuthError.SetActive(false);
    }

    void Authorize()
    {
        StartCoroutine(Authorize_Coroutine());
    }

    IEnumerator Authorize_Coroutine()
    {
        string url = "http://20.15.114.131:8080/api/login";  // URL for authentication endpoint
        string requestData = "{\"apiKey\": \"" + apiKey + "\"}";  // Create JSON request body with API key

        //POST Request
        using (UnityWebRequest request = UnityWebRequest.Post(url, new List<IMultipartFormSection> { new MultipartFormDataSection(requestData) }))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(requestData);  //Convert JSON to bytes
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");  //Request Header

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // If the request was successful, deserialize the token response
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(request.downloadHandler.text);
                
                if (!string.IsNullOrEmpty(tokenResponse.token)) {
                    GlobalManager_.Instance.SetJwtToken(tokenResponse.token); // Save JWT token globally
                    Debug.Log($"JWT Token: {tokenResponse.token}");

                    // Hide the Login button and show authentication success pop-up
                    authorizeButton.gameObject.SetActive(false);
                    PopUpAuth.SetActive(true);
                }
                else
                {
                    // Hide the Login button and show authentication error pop-up
                    authorizeButton.gameObject.SetActive(false);
                    PopUpAuthError.SetActive(true);
                    Debug.LogError(request.error);
                }
            }
            else
            {
                // Hide the authorize button and show authentication error pop-up
                authorizeButton.gameObject.SetActive(false);
                PopUpAuthError.SetActive(true);
                Debug.LogError(request.error);
            }
        }
    }

    // Serializable class for token response
    [System.Serializable]
    public class TokenResponse
    {
        public string token; // This maps to {"token":"<actual_token_value>"}
    }
}
