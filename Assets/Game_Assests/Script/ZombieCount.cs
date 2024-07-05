using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ZombieCount : MonoBehaviour
{
    public int zombieCount = 30;
    public TMP_Text scoreText;
    public GameObject PopUpWon;

    // Function to update the zombie count
    void Start()
    {
        PopUpWon.SetActive(false);
        StartCoroutine(CheckZombiesCoroutine());
    }
    public void UpdateZombieCount(int count)
    {
        zombieCount = count;
        scoreText.text = "" + zombieCount;

    }

    private IEnumerator CheckZombiesCoroutine()
    {
        while (true)
        {
            // Check if all zombies have been destroyed and zombieCount is 0
            if (this.zombieCount == 0 && AreAllZombiesDestroyed())
            {
                // Activate the PopUpWon object
                this.PopUpWon.SetActive(true);
                float score = 32 - GlobalVariable.Instance.elapsedTime;
                score = 1000/ score;
                double scaledScore = 100 / (1 + Math.Exp(-0.02 * (score - 50.0)));                
                GlobalManager_.Instance.SetLeaderboardScore((int)scaledScore);
                Debug.Log("Leaderboard Score: " + GlobalManager_.Instance.LeaderboardScore);


                // Stop the coroutine
                break;
            }

            // Wait for one frame before checking again
            yield return null;
        }
    }


    private bool AreAllZombiesDestroyed()
    {
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie_tag"))
        {
            if (zombie != null)
            {
                return false;
            }
        }
        return true;
    }
}
