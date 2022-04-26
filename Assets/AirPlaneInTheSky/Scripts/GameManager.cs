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
    float fuelConsume = 0.01f;

    int score = 0;

    bool isGOAudioPlaying = false;

    SpaceShip spaceShipScript;
    AudioSource generalSoundHandle;

    [SerializeField] float timeToGameOver = 60;
    [SerializeField] AudioClip itemCollectedSound;
    [SerializeField] AudioClip gameOverVoice;

    public GameObject player;
    public GameObject gameOverContainer;

    public Slider fuelTank;
    public Slider ammo;

    public TextMeshProUGUI timer;
    //public TextMeshProUGUI playerName;
    public TextMeshProUGUI scoreDisplayText;
    public TextMeshProUGUI gameOverScore;


    public bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");

        spaceShipScript = player.GetComponent<SpaceShip>();

        isGameActive = true;

        timer.text = timeToGameOver.ToString();

        //playerName.text = MainManager.Instance.PlayerName;
        
        gameOverContainer.gameObject.SetActive(false);

        fuelTank.value = 1;

        ammo.value = 1;

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
        player.GetComponent<SpaceShip>().enabled = false;
        player.GetComponent<SpaceShipController>().enabled = false;
        gameOverScore.text = /*playerName +*/ "Score: " + scoreDisplayText.text;
        gameOverContainer.gameObject.SetActive(true);
        GameObject.Find("Magic fire pro orange").GetComponent<ParticleSystem>().Stop();
        GameObject.Find("Cannon_1").GetComponent<Shoot>().enabled = false;
        GameObject.Find("Cannon_2").GetComponent<Shoot>().enabled = false;
        GameObject.Find("Crosshair").GetComponent<Image>().enabled = false;
        if (!isGOAudioPlaying)
        {
            StartCoroutine(GameOverVoice(2f));
            isGOAudioPlaying = true;
        }
        
    }
    IEnumerator GameOverVoice(float time)
    {
        yield return new WaitForSeconds(time);
        generalSoundHandle.PlayOneShot(gameOverVoice);
    }
    /*
     * 'CHECK' Functions for checking player resources and status
     */
    void CheckRemaingTime()
    {
        if (timeToGameOver > 0 && isGameActive)
        {
            timeToGameOver -= Time.deltaTime;

            timer.text = Mathf.RoundToInt(timeToGameOver).ToString();
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

        if (fuelTimer > 2 && isGameActive)
        {
            if (playerFuel <= 0)
            {
                GameOver();
                fuelTank.value = 0;
            }
            else
            {
                playerFuel = spaceShipScript.DecreaseFuel();
                fuelTimer = 0;
            }
        }

        fuelTank.value = playerFuel * fuelConsume;
    }

    void CheckPlayerAmmo()
    {
        int ammoAux = spaceShipScript.Ammo;

        float currentAmmoQtt = (ammoAux * 100) / spaceShipScript.AmmoCapacity;


        ammo.value = currentAmmoQtt * 0.01f;
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
        scoreDisplayText.text = score.ToString();
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
