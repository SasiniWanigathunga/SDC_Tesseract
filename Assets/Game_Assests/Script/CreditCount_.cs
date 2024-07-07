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
    private string daily_consumption_url = "http://20.15.114.131:8080/api/power-consumption/current-month/daily/view";
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
        int avg_monthly_consumption = 0;
        int avg_daily_consumption = 0;
        int  rewardFromCurrentDay = 0;
        int varReward = 0;
        DateTime now = DateTime.Now;

        // Print the current date
        int year = now.Year;
        int month = now.Month;
        int day = now.Day;
        
        Debug.Log("Current date: " + year + "-" + month + "-" + day);

        using (UnityWebRequest all_request = UnityWebRequest.Get(yearly_consumption_url))
        {
            all_request.SetRequestHeader("accept", "*/*");
            string jwtToken = GlobalManager_.Instance.JwtToken;
            string authHeaderValue = "Bearer " + jwtToken;
            all_request.SetRequestHeader("Authorization", authHeaderValue);
            yield return all_request.SendWebRequest();
            int total_months = 0;
            int pastConsumption = 0;
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
                    if (yearlyData.year <= year){
                        int counter_var = 1;
                        foreach (var monthData in yearlyData.units)
                        {
                            if (counter_var >= month && yearlyData.year == year){
                                break;
                            }
                            pastConsumption += (int)monthData.Value.units;
                            counter_var++;
                            total_months++;
                        }
                    }
                }
            } 
        

            
            avg_monthly_consumption = pastConsumption / total_months;
            Debug.Log("Average Monthly Consumption since last year: " + avg_monthly_consumption);

            using (UnityWebRequest daily_request = UnityWebRequest.Get(daily_consumption_url))
            {
                daily_request.SetRequestHeader("accept", "*/*");
                daily_request.SetRequestHeader("Authorization", authHeaderValue);
                yield return daily_request.SendWebRequest();
                if (daily_request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Daily_Consumption_API_is"+daily_request.error);
                }
                else
                {
                    string dailyJsonText = daily_request.downloadHandler.text;
                    Dictionary<string, DailyPowerConsumptionView> dailyPowerConsumptionDict = JsonConvert.DeserializeObject<Dictionary<string, DailyPowerConsumptionView>>(dailyJsonText);

                    // Extract dailyUnits data
                    Dictionary<string, float> dailyUnits = dailyPowerConsumptionDict["dailyPowerConsumptionView"].dailyUnits ?? new Dictionary<string, float>();

                    int counter = 1;
                    int accumulatedDailyConsumption = 0;
                    foreach (var dailyUnit in dailyUnits)
                    {
                        if (counter >= day){
                            break;
                        }
                        accumulatedDailyConsumption += (int)dailyUnit.Value;
                        counter++;
                    }
                    avg_daily_consumption = accumulatedDailyConsumption / counter;
                    Debug.Log("Average daily consumption: " + avg_daily_consumption);
                                
                }
                

                using (UnityWebRequest request = UnityWebRequest.Get(powerConsumptionUrl))
                {
                    request.SetRequestHeader("accept", "*/*");
                    request.SetRequestHeader("Authorization", authHeaderValue);
                    yield return request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log("Error: " + request.error);
                    }
                    else
                    {   
                        string consumptionJson = request.downloadHandler.text;
                        Dictionary<string, object> consumptionData = JsonConvert.DeserializeObject<Dictionary<string, object>>(consumptionJson);
                        float currentConsumption_ =  Convert.ToSingle(consumptionData["currentConsumption"]);
                        int currentConsumption = (int)currentConsumption_;

                        DateTime now_ = DateTime.Now; 
                        DateTime startOfDay = now_.Date;
                        TimeSpan elapsedTime = now_ - startOfDay;
                        int elapsedMinutes = (int)elapsedTime.TotalMinutes;
                        rewardFromCurrentDay = currentConsumption/elapsedMinutes;
                        Debug.Log("Average reward from current day: " + rewardFromCurrentDay);

                        
                        for (int c = 0; c <= 15; c++)
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
                                        varReward = varReward + 100;
                                    }

                                    cumulativeReward = avg_monthly_consumption + avg_daily_consumption + rewardFromCurrentDay + varReward;
                                    Debug.Log("Cumulative reward: " + cumulativeReward);          
                                    
                                }
                            }
                            yield return new WaitForSeconds(10);
                            

                        }
                        
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

    public class DailyPowerConsumptionView
    {
        public int year { get; set; }
        public int month { get; set; }
        public Dictionary<string, float> dailyUnits { get; set; }
    }
    public class RootDaily
    {
        public Dictionary<string, DailyPowerConsumptionView> dailyPowerConsumptionViews { get; set; }
        
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
