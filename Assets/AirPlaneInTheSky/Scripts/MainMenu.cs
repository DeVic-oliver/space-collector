using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
public class MainMenu : MonoBehaviour
{
    string targetScene;
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        targetScene = "Infinite";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(playerNameInput.text) || playerNameInput.text.Length > 20)
        {
            Debug.Log("ERROR IN INPUT FIELD");
        }
        else
        {
            MainManager.Instance.PlayerName = playerNameInput.text;
            LoadingData.sceneToLoad = targetScene;
            SceneManager.LoadScene("LoadingScene");
        }
        
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
