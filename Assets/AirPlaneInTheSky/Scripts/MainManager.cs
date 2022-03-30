using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    string playerName;

    public string PlayerName
    {
        get { return playerName; }
        set
        {
            if(value == "")
            {
                Debug.Log("Error");   
            }
            else
            {
                playerName = value;
            }
        }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
