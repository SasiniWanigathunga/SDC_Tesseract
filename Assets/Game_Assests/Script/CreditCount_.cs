using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class CreditCount_ : MonoBehaviour
{
    public int creditCount = 0;
    public TMP_Text creditText;
    private string powerConsumptionUrl = "http://20.15.114.131:8080/api/power-consumption/current/view";
    private string yearly_consumption_url = "http://20.15.114.131:8080/api/power-consumption/all/view";
    int cumulativeReward = 0;

    void Start()
    {
        GlobalManager_.Instance.SetCreditConsumption(0);
        StartCoroutine(GetCurrentConsumptionAndUpdateScore());
    }

    void Update()
    {
        // Update creditText
        creditCount = cumulativeReward - GlobalManager_.Instance.CreditCosumption;
        GlobalManager_.Instance.SetCreditCount(creditCount);
        creditText.text = "" + GlobalManager_.Instance.CreditCount.ToString();
    }
    
    
    IEnumerator GetCurrentConsumptionAndUpdateScore()
    {
        int pastConsumption = 0; // Implement your logic here
        DateTime now = DateTime.Now;

        // Print the current date
        
        Debug.Log("Current date: " + now.ToString("yyyy-MM-dd"));

        using (UnityWebRequest all_request = UnityWebRequest.Get(yearly_consumption_url))
        {
            all_request.SetRequestHeader("accept", "*/*");
            string jwtToken = GlobalManager_.Instance.JwtToken;
            Debug.Log("JWT Token: " + jwtToken);
            string authHeaderValue = "Bearer " + jwtToken;
            Debug.Log("Authorization: " + authHeaderValue);
            all_request.SetRequestHeader("Authorization", authHeaderValue);
            yield return all_request.SendWebRequest();
            
            if (all_request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Past_Consumption_API_is"+all_request.error);
            }
            else
            {
                string response = all_request.downloadHandler.text;
                Root deserializedResponse = JsonConvert.DeserializeObject<Root>(response);

                // Now you can access your data as a dictionary, for example:
                foreach (var yearlyData in deserializedResponse.yearlyPowerConsumptionViews)
                {
                    Debug.Log("Year: " + yearlyData.year);
                    foreach (var monthData in yearlyData.units)
                    {
                        Debug.Log(monthData.Key + ": " + monthData.Value.units);
                    }
                }
            } 
        

            int rewardFromPast = (int)(100000 / (1000 + pastConsumption));
            Debug.Log("Reward from past: " + rewardFromPast);


            using (UnityWebRequest request = UnityWebRequest.Get(powerConsumptionUrl))
            {
                request.SetRequestHeader("accept", "*/*");
                //string jwtToken = GlobalManager_.Instance.JwtToken;
                Debug.Log("JWT Token: " + jwtToken);
                //string authHeaderValue = "Bearer " + jwtToken;
                Debug.Log("Authorization: " + authHeaderValue);
                request.SetRequestHeader("Authorization", authHeaderValue);
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Error: " + request.error);
                }
                else
                {   
                    Debug.Log("Current consumption: " + request.downloadHandler.text);


                    string consumptionJson = request.downloadHandler.text;
                    Dictionary<string, object> consumptionData = JsonConvert.DeserializeObject<Dictionary<string, object>>(consumptionJson);
                    float currentConsumption_ =  Convert.ToSingle(consumptionData["currentConsumption"]);

                    int currentConsumption = (int)currentConsumption_;
                    int rewardFromCurrentDay = 100000 / (1000 + currentConsumption);
                    Debug.Log("Reward from current: " + rewardFromCurrentDay);

                    int varReward = 0;

                    for (int c = 0; c <= 6; c++)
                    {
                        
                        using (UnityWebRequest realtimeRequest = UnityWebRequest.Get(powerConsumptionUrl))
                        {
                            realtimeRequest.SetRequestHeader("accept", "*/*");
                            realtimeRequest.SetRequestHeader("Authorization", authHeaderValue);
                            yield return realtimeRequest.SendWebRequest();

                            if (realtimeRequest.result != UnityWebRequest.Result.Success)
                            {
                                Debug.Log("Error: " + realtimeRequest.error);
                            }
                            else
                            {
                                string consumptionJsonTemp = realtimeRequest.downloadHandler.text;
                                Dictionary<string, object> consumptionDataTeamp = JsonConvert.DeserializeObject<Dictionary<string, object>>(consumptionJsonTemp);
                                float currentConsumptionTemp =  Convert.ToSingle(consumptionDataTeamp["currentConsumption"]);
                                int realtimeConsumption = (int)currentConsumptionTemp;
                                int consumptionDifference = realtimeConsumption - currentConsumption;
                                currentConsumption = realtimeConsumption;
                                Debug.Log("Consumption difference: " + consumptionDifference);

                                if (consumptionDifference < 0.5)
                                {
                                    varReward++;
                                }

                                cumulativeReward = rewardFromPast + rewardFromCurrentDay + varReward;
                                Debug.Log("Cumulative reward: " + cumulativeReward);          
                                
                            }
                        }
                        yield return new WaitForSeconds(10);
                        

                    }
                    
                }
            } 
        }
    }

    public class Units
    {
        public float units { get; set; }
    }

    public class YearlyPowerConsumptionView
    {
        public int year { get; set; }
        public Dictionary<string, Units> units { get; set; }
    }

    public class Root
    {
        public List<YearlyPowerConsumptionView> yearlyPowerConsumptionViews { get; set; }
    }

//     IEnumerator GetPastConsumption()
//     {
//         // Implement your logic here
//         using (UnityWebRequest request = UnityWebRequest.Get(yearly_consumption_url))
//         {
//             request.SetRequestHeader("accept", "*/*");
//             string jwtToken = GlobalManager_.Instance.JwtToken;
//             Debug.Log("JWT Token: " + jwtToken);
//             string authHeaderValue = "Bearer " + jwtToken;
//             Debug.Log("Authorization: " + authHeaderValue);
//             request.SetRequestHeader("Authorization", authHeaderValue);
//             yield return request.SendWebRequest();
            
//             if (request.result != UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("Past_Consumption_API_is"+request.error);
//             }
//             else
//             {
//                 string response = request.downloadHandler.text;
//                 Root deserializedResponse = JsonConvert.DeserializeObject<Root>(response);

//                 // Now you can access your data as a dictionary, for example:
//                 foreach (var yearlyData in deserializedResponse.yearlyPowerConsumptionViews)
//                 {
//                     Debug.Log("Year: " + yearlyData.year);
//                     foreach (var monthData in yearlyData.units)
//                     {
//                         Debug.Log(monthData.Key + ": " + monthData.Value.units);
//                     }
//                 }
//             }
//             // if (request.result != UnityWebRequest.Result.Success)
//             // {
//             //     Debug.Log("Error: " + request.error);
//             // }
//             // else
//             // {
//             //     string consumptionJson = request.downloadHandler.text;
//             //     Dictionary<string, object> consumptionData = JsonConvert.DeserializeObject<Dictionary<string, object>>(consumptionJson);
//             //     float currentConsumption_ =  Convert.ToSingle(consumptionData["currentConsumption"]);

//             //     int currentConsumption = (int)currentConsumption_;
//             //     return currentConsumption;
//             // }
//         }
//         return 0;
//     }
}
