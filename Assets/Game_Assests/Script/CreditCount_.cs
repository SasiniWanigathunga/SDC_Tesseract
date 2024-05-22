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
    int cumulativeReward = 0;

    void Start()
    {
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
        int pastConsumption = GetPastConsumption(); // Implement your logic here

        int rewardFromPast = (int)(100000 / (1000 + pastConsumption));
        Debug.Log("Reward from past: " + rewardFromPast);


        using (UnityWebRequest request = UnityWebRequest.Get(powerConsumptionUrl))
        {
            request.SetRequestHeader("accept", "*/*");
            string jwtToken = GlobalManager_.Instance.JwtToken;
            Debug.Log("JWT Token: " + jwtToken);
            string authHeaderValue = "Bearer " + jwtToken;
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

                            // Update creditCount and creditText
                            
                        }
                    }
                    yield return new WaitForSeconds(10);
                    

                    // UnityWebRequest realtimeRequest = new UnityWebRequest(powerConsumptionUrl, "GET");
                    // realtimeRequest.SetRequestHeader("Authorization", "Bearer " + jwtToken);

                    // yield return realtimeRequest.SendWebRequest();

                    // if (realtimeRequest.result != UnityWebRequest.Result.Success)
                    // {
                    //     Debug.Log("Error: " + realtimeRequest.error);
                    // }
                    // else
                    // {
                    //     int realtimeConsumption = int.Parse(realtimeRequest.downloadHandler.text);
                    //     int consumptionDifference = realtimeConsumption - currentConsumption;
                    //     currentConsumption = realtimeConsumption;
                    //     Debug.Log("Consumption difference: " + consumptionDifference);

                    //     if (consumptionDifference < 0.5)
                    //     {
                    //         varReward++;
                    //     }

                    //     int cumulativeReward = rewardFromPast + rewardFromCurrentDay + varReward;
                    //     Debug.Log("Cumulative reward: " + cumulativeReward);

                    //     // Update creditCount and creditText
                    //     creditCount = cumulativeReward;
                    //     creditText.text = "Credits: " + creditCount.ToString();
                    // }
                }
                
            }
        }

        //UnityWebRequest www = new UnityWebRequest(powerConsumptionUrl, "GET");
        //www.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        //yield return www.SendWebRequest();

        
    }

    int GetPastConsumption()
    {
        // Implement your logic here
        return 0;
    }
}
