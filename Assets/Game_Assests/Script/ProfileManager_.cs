using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System;

public class ProfileManager_ : MonoBehaviour
{
    // Input fields for profile information
    public TMP_InputField firstNameInput;
    public TMP_InputField lastNameInput;
    public TMP_InputField nicInput;
    public TMP_InputField usernameInput;
    public TMP_InputField mobileNumberInput;
    public TMP_InputField emailInput;
    public Toggle demandResponseToggle;

    public GameObject canvas2;

    // Pop-up panels for error and success messages
    public TMP_Text popUpAuthError_Text;
    public GameObject PopUpAuthError;
    public GameObject PopUpAuth;
    public GameObject PopUpIncompleteProfile;

    // Text objects for displaying questionnaire score and earned lawn mowers
    public TMP_Text questionnaireScore;
    public TMP_Text lawnMowersEarned;

    //lawn mowers
    public GameObject imageCover1;
    public GameObject imageCover2;
    public GameObject imageCover3;
    public GameObject imageCover4;
    public GameObject imageCover5;

    void Start()
    {
        // Initialize input fields
        firstNameInput = GameObject.Find("Canvas2/FirstName/FirstName_Input").GetComponent<TMP_InputField>();
        lastNameInput = GameObject.Find("Canvas2/LastName/LastName_Input").GetComponent<TMP_InputField>();
        nicInput = GameObject.Find("Canvas2/NIC/NIC_Input").GetComponent<TMP_InputField>();
        usernameInput = GameObject.Find("Canvas2/Username/Username_Input").GetComponent<TMP_InputField>();
        mobileNumberInput = GameObject.Find("Canvas2/MobileNumber/MobileNumber_Input").GetComponent<TMP_InputField>();
        emailInput = GameObject.Find("Canvas2/EmailAddress/EmailAddress_Input").GetComponent<TMP_InputField>();
        demandResponseToggle = GameObject.Find("Canvas2/Demand/Demand_Input").GetComponent<Toggle>();

        //Hide Pop Up panels initially
        PopUpAuthError.SetActive(false);
        PopUpAuth.SetActive(false);
        PopUpIncompleteProfile.SetActive(false);
    }

    // Fetch player profile from the server
    public void FetchProfile()
    {
        StartCoroutine(FetchPlayerProfile());
    }

    // Update player profile
    public void UpdateProfile()
    {
        StartCoroutine(UpdatePlayerProfile());
    }

    // Close the profile view (If profile is not completed user is not allowed to close the Profile)
    public void CloseProfile()
    {
        // Extract individual properties from the input fields
        string firstName = firstNameInput.text;
        string lastName = lastNameInput.text;
        string NIC = nicInput.text;
        string phoneNumber = mobileNumberInput.text;
        string email = emailInput.text;

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(NIC) && !string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(email))
        {
            canvas2.SetActive(false);
        }
        else
        {
            PopUpIncompleteProfile.SetActive(true);  // Show incomplete profile pop-up
        }
    }
    

    IEnumerator FetchPlayerProfile()
    {
        // Update questionnaire score and earned lawn mowers UI elements
        int score = GlobalManager_.Instance.Score;
        questionnaireScore.text = score.ToString() + "/10";
        int lawnMowers = Mathf.FloorToInt(score / 2);
        lawnMowersEarned.text = "Lawn Mowers Earned : " + lawnMowers.ToString();
        List<GameObject> imageCovers = new List<GameObject> { imageCover1, imageCover2, imageCover3, imageCover4, imageCover5 };

        // Disable SetActivate for a number of lawn mowers
        for (int i = 0; i < lawnMowers && i < imageCovers.Count; i++)
        {
            if (imageCovers[i] != null)
            {
                imageCovers[i].SetActive(false);
            }
        }

        string url = "http://20.15.114.131:8080/api/user/profile/view";  // URL for profile view endpoint

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
                string profileJson = request.downloadHandler.text;

                // Populate profile input fields with retrieved profile data
                PopulateProfileFields(profileJson);  
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    public void PopulateProfileFields(string profileJson)
    {
        // Deserialize the JSON string into a Dictionary<string, object>
        Dictionary<string, object> profileData = JsonConvert.DeserializeObject<Dictionary<string, object>>(profileJson);
        string userJson = JsonConvert.SerializeObject(profileData["user"]);
        Dictionary<string, object> userData = JsonConvert.DeserializeObject<Dictionary<string, object>>(userJson);

        string lastname = "";
        string email = "";

        //lastname can be null
        if (userData.ContainsKey("lastname") && userData["lastname"] != null)
        {
            lastname = userData["lastname"].ToString();
        }

        //email can be null
        if (userData.ContainsKey("email") && userData["email"] != null)
        {
            email = userData["email"].ToString();
        }

        string firstname = userData.ContainsKey("firstname") ? userData["firstname"].ToString() : "";
        string nic = userData.ContainsKey("nic") ? userData["nic"].ToString() : "";
        string username = userData.ContainsKey("username") ? userData["username"].ToString() : "";
        string mobilenumber = userData.ContainsKey("phoneNumber") ? userData["phoneNumber"].ToString() : "";
        bool demandresponse = demandResponseToggle != null ? demandResponseToggle.isOn : userData.ContainsKey("demandResponseProgramMemberStatus") && (bool)userData["demandResponseProgramMemberStatus"];
        //bool demandresponse = demandresponse != null ? demandresponse : userData.ContainsKey("demandResponseProgramMemberStatus") && (bool)userData["demandResponseProgramMemberStatus"] || demandResponseToggle.isOn;

        // Populate the input fields with the retrieved profile data
        firstNameInput.text = firstname;
        lastNameInput.text = lastname;
        nicInput.text = nic;
        usernameInput.text = username;
        mobileNumberInput.text = mobilenumber;
        emailInput.text = email;
        demandResponseToggle.isOn = demandresponse;
    }

    IEnumerator UpdatePlayerProfile()
    {
        string url = "http://20.15.114.131:8080/api/user/profile/update";

        // Extract individual properties from the input fields
        string firstName = firstNameInput != null ? firstNameInput.text : "";
        string lastName = lastNameInput != null ? lastNameInput.text : "";
        string NIC = nicInput != null ? nicInput.text : "";
        string phoneNumber = mobileNumberInput != null ? mobileNumberInput.text : ""; // Assuming mobileNumberInput represents phoneNumber
        string email = emailInput != null ? emailInput.text : "";
        bool demandresponse = demandResponseToggle != null ? demandResponseToggle.isOn : false;

        // Create a JSON string for the request body
        string requestData = JsonConvert.SerializeObject(new
        {
            firstname = firstName,
            lastname = lastName,
            nic = NIC,
            phoneNumber = phoneNumber,
            email = email
        });

        Debug.Log(requestData);

        // Convert the JSON data to bytes
        byte[] byteData = System.Text.Encoding.UTF8.GetBytes(requestData);

        // Create a PUT request with the JSON data
        using (UnityWebRequest request = UnityWebRequest.Put(url, requestData))
        {
            request.SetRequestHeader("accept", "*/*");
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log(GlobalManager_.Instance.JwtToken);
            request.SetRequestHeader("Authorization", "Bearer " + GlobalManager_.Instance.JwtToken);
            Debug.Log(request);
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                PopUpAuth.SetActive(true);
                string newprofileJson = request.downloadHandler.text;

                // Populate the input fields with the retrieved profile data
                PopulateProfileFields(newprofileJson);
            }
            else
            {
                // Log an error if the request fails
                Debug.LogError("Failed to update profile: " + request.error);
                Dictionary<string, object> responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(request.downloadHandler.text);
                Debug.Log(responseData["message"]);
                PopUpAuthError.SetActive(true);
                popUpAuthError_Text.text = responseData["message"].ToString();
            }
        }
    }
}

