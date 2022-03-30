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
    float playerFuel;

    [SerializeField] float timeToGameOver = 60;

    public GameObject player;

    public GameObject gameOverContainer;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI fuelTank;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI gameOverScore;


    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

        timer.text = "Time: " + timeToGameOver + "s";

        playerName.text = MainManager.Instance.PlayerName;
        
        gameOverContainer.gameObject.SetActive(false);

        spawnManager = GameObject.Find("SpawnManager");

        fuelTank.text = "Fuel: " + player.GetComponent<SpaceShip>().fuelTank + "%";

        ammo.text = "Ammo: " + player.GetComponent<SpaceShip>().ammo;

        SetCursorVisibility(false);
    }


    // Update is called once per frame
    void Update()
    {
        CheckRemaingTime();

        CheckFuelTank();

        CheckPlayerStatus();
    }

    public void GameOver()
    {
        isGameActive = false;
        SetCursorVisibility(true);
        gameOverScore.text = player.GetComponent<Count>().countText.text;
        gameOverContainer.gameObject.SetActive(true);
   
    }

    void CheckRemaingTime()
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
    }

    void CheckFuelTank()
    {
        playerFuel = player.GetComponent<SpaceShip>().fuelTank;

        fuelTimer += Time.deltaTime; // fuelTimer is used to decrease fuel of the spaceship after a certain amout of time

        if (fuelTimer > 2)
        {
            if (playerFuel <= 0)
            {
                GameOver();
                fuelTank.text = "Fuel: " + 0f + "%";
            }
            else
            {
                playerFuel = player.GetComponent<SpaceShip>().DecreaseFuel();
                fuelTimer = 0;
            }
        }

        fuelTank.text = "Fuel: " + playerFuel + "%";

        ammo.text = "Ammo: " + player.GetComponent<SpaceShip>().ammo;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetCursorVisibility(false);
    }

    void SetCursorVisibility(bool visibility = true)

    {
        if (visibility == true)
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

    void CheckPlayerStatus()
    {
        if (!player.GetComponent<SpaceShip>().isAlive)
        {
            GameOver();
        }
    }
}
