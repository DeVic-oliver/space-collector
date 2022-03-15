using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject spawnManager;

    float fuelTimer = 0;

    [SerializeField] float timeToGameOver = 60;

    public GameObject player;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI fuelTank;
    public TextMeshProUGUI ammo;
    public Button restartButton;

    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

        timer.text = "Time: " + timeToGameOver + "s";
        
        gameOverText.gameObject.SetActive(false);
        
        restartButton.gameObject.SetActive(false);
        
        spawnManager = GameObject.Find("SpawnManager");

        fuelTank.text = "Fuel Tank: " + player.GetComponent<SpaceShip>().fuelTank + "%";

        ammo.text = "Ammo: " + player.GetComponent<SpaceShip>().ammo;

        SetCursorVisibility(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (timeToGameOver > 0 && isGameActive)
        {
            timeToGameOver -= Time.deltaTime;

            timer.text = "Time: " + Mathf.RoundToInt(timeToGameOver); ;
        }
        else
        {
            GameOver();
            player.GetComponent<SpaceShipController>().enabled = false;
            spawnManager.GetComponent<SpawnManager>().enabled = false;
        }
        
       float playerFuel = player.GetComponent<SpaceShip>().fuelTank;

        fuelTimer += Time.deltaTime;
       
        if (fuelTimer > 2)
        {
            if(playerFuel <= 0)
            {
                GameOver();
                fuelTank.text = "Fuel Tank: " + 0f + "%";
            }
            else
            {
                playerFuel = player.GetComponent<SpaceShip>().DecreaseFuel();
                fuelTimer = 0;
            }
        }

        fuelTank.text = "Fuel Tank: " + playerFuel + "%";

        ammo.text = "Ammo: " + player.GetComponent<SpaceShip>().ammo;


    }

    void GameOver()
    {
        isGameActive = false;
        SetCursorVisibility(true);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetCursorVisibility(false);
    }

    void SetCursorVisibility(bool visibility = true)

    {
        if(visibility == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = visibility;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = visibility;
        }
    }
}
