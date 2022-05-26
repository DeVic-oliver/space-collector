using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    float fuelTimer = 0;
    float playerFuel;
    float fuelAux;
    float fuelConsume = 0.01f;
    float warningTimerCount = 10;

    int score = 0;

    bool isGOAudioPlaying = false;
    bool isWorldAudioMuted = false;

    string playerName;

    SpaceShip spaceShipScript;
    AudioSource generalSoundHandle;
    GameObject spawnManager;
    Collider limitBoxColider;

    [SerializeField] float timeToGameOver = 60;
    [SerializeField] float bonusTime = 25;
    [SerializeField] AudioClip itemCollectedSound;
    [SerializeField] AudioClip gameOverVoice;
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject limitBox;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI warningTimer;

    public static bool isGamePaused = false;

    public GameObject player;
    public GameObject gameOverContainer;

    public Slider fuelTank;
    public Slider ammo;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI scoreDisplayText;
    public TextMeshProUGUI gameOverScore;

    public static bool isGameActive = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager");

        spaceShipScript = player.GetComponent<SpaceShip>();

        isGameActive = true;

        timer.text = timeToGameOver.ToString();

        playerName = MainManager.Instance.PlayerName;

        gameOverContainer.gameObject.SetActive(false);

        fuelTank.value = 1;

        ammo.value = 1;

        generalSoundHandle = GetComponent<AudioSource>();

        limitBoxColider = limitBox.GetComponent<Collider>();


        SetCursorVisibility(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }

        if (!isGamePaused && isGameActive) {
            CheckRemaingTime();

            CheckFuelTank();

            CheckPlayerStatus();

            CheckPlayerAmmo();

            CheckWorldSound();

            SetCursorVisibility(false);
        }
        else
        {
            SetCursorVisibility(true);
        }

        if (!isGameActive)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;

        SetCursorVisibility(true);

        player.GetComponent<SpaceShip>().enabled = false;

        player.GetComponent<SpaceShipController>().enabled = false;

        gameOverScore.text = playerName + ": " + scoreDisplayText.text;

        gameOverContainer.gameObject.SetActive(true);

        DisableGameObjectChilds("Turbine Container");

        DisableGameObjectChilds("Cannon Container");

        GameObject.Find("Crosshair").GetComponent<Image>().enabled = false;

        if (!isGOAudioPlaying)
        {
            StartCoroutine(GameOverVoice(2f));
            isGOAudioPlaying = true;
        }

    }

    void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            pauseMenu.SetActive(false);
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
                //fuelAux = playerFuel;
                spaceShipScript.DecreaseFuel();
                fuelTank.value -= fuelConsume;
            }
            fuelTimer = 0;
        }

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

        if (!limitBoxColider.bounds.Contains(player.transform.position) && player.GetComponent<SpaceShip>().isAlive )
        {
            warningPanel.SetActive(true);
            warningTimer.text = warningTimerCount.ToString("F1");
            warningTimerCount-=Time.deltaTime;
            if(warningTimerCount <= 0)
            {
                warningPanel.SetActive(false);
                GameOver();
            }
        }
        else
        {
            warningPanel.SetActive(false);
            warningTimerCount = 10;
        }
    }
    //-----------------------------------------------------
    void CheckWorldSound()
    {
        if (!isWorldAudioMuted)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }
    void PlaySound(AudioClip audioClip)
    {
        generalSoundHandle.PlayOneShot(audioClip, 0.1f);
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

    void DisableGameObjectChilds(string gameObjectName)
    {
        GameObject parent = GameObject.Find(gameObjectName);

        int childsCount = parent.transform.childCount;

        for (int i = 0; i < childsCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    IEnumerator GameOverVoice(float time)
    {
        yield return new WaitForSeconds(time);
        generalSoundHandle.PlayOneShot(gameOverVoice);
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

    public void AddTime()
    {
        PlaySound(itemCollectedSound);
        timeToGameOver += bonusTime;

        timer.text = Mathf.RoundToInt(timeToGameOver).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetCursorVisibility(false);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        PauseGame();
    }

    public void Mute()
    {
        if (isWorldAudioMuted)
        {
            isWorldAudioMuted = false;
        }
        else
        {
            isWorldAudioMuted = true;
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
