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
