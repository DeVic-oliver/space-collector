using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene(1);
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
