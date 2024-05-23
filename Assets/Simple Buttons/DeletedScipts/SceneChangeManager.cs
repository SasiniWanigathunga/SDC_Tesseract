using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangetoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangetoGameScene()
    {
        SceneManager.LoadScene("GameScene");
        GlobalVariable.Instance.SetElapsedTime(Time.time);
    }

    public void ChangetoPreGameScene()
    {
        SceneManager.LoadScene("Pre_GameScene");
    }
}
