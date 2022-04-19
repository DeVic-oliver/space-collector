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

    int score = 0;
    int playerAmmo;

    bool isGOAudioPlaying = false;

    SpaceShip spaceShipScript;
    AudioSource generalSoundHandle;

    [SerializeField] float timeToGameOver = 60;
    [SerializeField] AudioClip itemCollectedSound;
    [SerializeField] AudioClip gameOverVoice;

    public GameObject player;
    public GameObject gameOverContainer;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI fuelTank;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI scoreDisplayText;
    public TextMeshProUGUI gameOverScore;


    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");

        spaceShipScript = player.GetComponent<SpaceShip>();

        isGameActive = true;

        timer.text = "Time: " + timeToGameOver + "s";

        //playerName.text = MainManager.Instance.PlayerName;
        
        gameOverContainer.gameObject.SetActive(false);

        fuelTank.text = "Fuel: " + spaceShipScript.FuelTank + "%";

        ammo.text = "Ammo: " + spaceShipScript.Ammo;

        generalSoundHandle = GetComponent<AudioSource>();
        
        SetCursorVisibility(false);
    }


    // Update is called once per frame
    void Update()
    {
        CheckRemaingTime();

        CheckFuelTank();

        CheckPlayerStatus();

        CheckPlayerAmmo();

    }

    public void GameOver()
    {
        isGameActive = false;
        SetCursorVisibility(true);
        gameOverScore.text = scoreDisplayText.text;
        gameOverContainer.gameObject.SetActive(true);
        if (!isGOAudioPlaying)
        {
            //gameOverContainer.GetComponent<AudioSource>().Play();
            generalSoundHandle.PlayOneShot(gameOverVoice);
            isGOAudioPlaying = true;
        }
        
    }

    /*
     * 'CHECK' Functions for checking player resources and status
     */
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
        playerFuel = spaceShipScript.FuelTank;

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
                playerFuel = spaceShipScript.DecreaseFuel();
                fuelTimer = 0;
            }
        }

        fuelTank.text = "Fuel: " + playerFuel + "%";
    }

    void CheckPlayerAmmo()
    {
        ammo.text = "Ammo: " + spaceShipScript.Ammo;
    }

    void CheckPlayerStatus()
    {
        if (!player.GetComponent<SpaceShip>().isAlive)
        {
            GameOver();
        }
    }

    void PlaySound(AudioClip audioClip)
    {
        generalSoundHandle.PlayOneShot(audioClip, 0.1f);
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

    public void AddScore() 
    {
        PlaySound(itemCollectedSound);
        score++;
        scoreDisplayText.text = "Score: " + score;
    }

    public void AddFuel()
    {
        PlaySound(itemCollectedSound);
        int spaceShipfFuel = spaceShipScript.FuelTank;
        int fuelTotal = spaceShipfFuel + 25;
        if (fuelTotal >= 100)
        {
            spaceShipfFuel = 100;
        }
        else
        {
            spaceShipfFuel += 25;
        }
        spaceShipScript.FuelTank = spaceShipfFuel;
    }

    public void AddAmmo()
    {
        PlaySound(itemCollectedSound);
        int ammoCapacity = spaceShipScript.AmmoCapacity;
        int ammoTotal = spaceShipScript.Ammo + 50;
        if (ammoTotal >= ammoCapacity)
        {
            ammoTotal = ammoCapacity;
        }
        else
        {
            spaceShipScript.Ammo = ammoTotal;
        }
    }
}
