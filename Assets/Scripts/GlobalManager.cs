using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }
    public string JwtToken { get; private set; }  // JWT token for authentication
    public int Score { get; private set; } // New global variable for the score

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the instance alive across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensures only one instance is active
        }
    }

    // Method to set the JWT token
    public void SetJwtToken(string token)
    {
        JwtToken = token;
    }

    // Method to set the player's score
    public void SetScore(int score)
    {
        Score = score;
    }
}
