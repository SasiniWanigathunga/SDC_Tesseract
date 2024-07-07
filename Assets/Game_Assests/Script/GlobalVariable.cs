using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable instance;

    public bool isLost;
    public float elapsedTime;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public static GlobalVariable Instance
    {
        get
        {
            return instance;
        }
    }
    public void SetIsLost(bool value)
    {
        isLost = value;
    }
    public void SetElapsedTime(float value)
    {
        elapsedTime = value;
    }
}
