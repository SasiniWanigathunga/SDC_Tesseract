using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayAccessManager : MonoBehaviour
{
    public Button playButton;
    public GameObject PopUpPlay;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("PlayButton").GetComponent<Button>().onClick.AddListener(Play);
        PopUpPlay.SetActive(false);

    }

    public void ChangetoPreGameScene()
    {
        SceneManager.LoadScene("Pre_GameScene");
    }

    void Play()
    {
        // bool quizAttempted = GlobalManager_.Instance.QuizAttempted;
        bool quizAttempted = true;
        if (quizAttempted)
        {
            ChangetoPreGameScene();
        }
        else
        {
            PopUpPlay.SetActive(true);
        }
    }
}
