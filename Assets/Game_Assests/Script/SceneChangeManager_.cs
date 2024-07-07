using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeManager_ : MonoBehaviour
{
    [SerializeField] private GameObject starting_transistion;
    [SerializeField] private GameObject ending_transistion;
    private static string previousSceneName;

    // Start is called before the first frame update
    void Start()
    {
        starting_transistion.SetActive(false);
        ending_transistion.SetActive(false);
        string current_scene = SceneManager.GetActiveScene().name;
        if (current_scene == "MainMenu" && previousSceneName == "MainScene")
        {
            ending_transistion.SetActive(true);
        }

        previousSceneName = SceneManager.GetActiveScene().name;



    }

    // Update is called once per frame


    public void ChangetoMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            StartCoroutine(LoadMainMenu());
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        //
    }

    public IEnumerator LoadMainMenu()
    {
        starting_transistion.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangetoGameScene()
    {
        SceneManager.LoadScene("GameScene");
        GlobalVariable.Instance.SetElapsedTime(Time.time);
        GlobalVariable.Instance.SetIsLost(false);
    }

    public void ChangetoPreGameScene()
    {
        SceneManager.LoadScene("Pre_GameScene");   
    }


    public void ChangetoLeaderboardScene()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }
}
