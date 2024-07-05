using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserView
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public object Nic { get; set; }
        public object PhoneNumber { get; set; }
        public object Email { get; set; }
        public object Score { get; set; }
    }

public class GlobalManager_ : MonoBehaviour
{
    public static GlobalManager_ Instance { get; private set; }
    public string JwtToken { get; private set; }  // JWT token for authentication
    public int Score { get; private set; } // New global variable for the score
    public bool QuizAttempted { get; private set; } // New global variable for the quiz attempted status

    public int CreditCount { get; private set; } // New global variable for the credit count
    public int CreditCosumption { get; private set; } // New global variable for the credit consumption
    public int UpdateScore { get; private set; } // New global variable for the updated score

    public int LeaderboardScore { get; private set; } // New global variable for the leaderboard score

    public List<UserView> UserViews { get; set; } // New global variable for the user views

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the instance alive across scenes
            QuizAttempted = false; // Set the quiz attempted status to false
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

    public void SetQuizAttempted(bool quizattempted)
    {
        QuizAttempted = quizattempted;
    }

    // Method to set the credit count
    public void SetCreditCount(int count)
    {
        CreditCount = count;
    }

    // Method to set the credit consumption
    public void SetCreditConsumption(int consumption)
    {
        CreditCosumption = consumption;
    }

    // Method to set the updated score
    public void SetUpdateScore(int updateScore)
    {
        UpdateScore = updateScore;
    }

    // Method to set the user views
    public void SetUserViews(List<UserView> userViews)
    {
        UserViews = userViews;
    }

    public void SetLeaderboardScore(int leaderboardScore)
    {
        LeaderboardScore = leaderboardScore;
    }
}
