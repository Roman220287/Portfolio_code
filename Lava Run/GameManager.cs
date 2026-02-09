using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject pauseScreen;
    public GameObject titleScreen;
    public GameObject credits;
    public TMP_Text yourTime;

    //For Falling rocks from vulcano
    public bool vulcanoCutSceneHappend;
    public Transform[] rockSpawnPoint;
    [SerializeField] private GameObject[] rocks;
    [SerializeField] private CameraSwap _camSwap;
    private float _rockTimer;
    

    GameManager()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        if (vulcanoCutSceneHappend)
        {
            if (_rockTimer > 0)                 
            {
                _rockTimer -= Time.deltaTime;
                if (_rockTimer <= 0)
                {
                    ThrowRock();
                }
            }
        }

        GetTime();
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitToDesktop()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        titleScreen.SetActive(false);
        credits.SetActive(true);
    }

    public void Back()
    {
        titleScreen.SetActive(true);
        credits.SetActive(false);
    }

    public void StartRockThrowTimer(float _timer)
    {
        _rockTimer = _timer;
    }
    public void ThrowRock()
    {
        Instantiate(rocks[Random.Range(0, rocks.Length)], rockSpawnPoint[Random.Range(0,rockSpawnPoint.Length)]);
        StartRockThrowTimer(1);
    }


    public void GetTime()
    {
        float loadedTime = PlayerPrefs.GetFloat("finishedTime");
        int minutes = Mathf.FloorToInt(loadedTime / 60);
        int seconds = Mathf.FloorToInt(loadedTime % 60);
        yourTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
