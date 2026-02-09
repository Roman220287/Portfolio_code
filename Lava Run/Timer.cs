using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timer;
    public float totalTime = 60.0f;
    public GameObject gameoverMenu;
    public float _currentTime;
    public bool levelCompleted;


    void Start()
    {
        _currentTime = totalTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (GameManager.instance.vulcanoCutSceneHappend)
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerText();
        }

        else
        {
            _currentTime = 0;
        }

        if (_currentTime == 0)
        {
            gameoverMenu.SetActive(true);
        }
    }

    //public void StopTimer()
    //{
    //    levelCompleted = true;
    //}

    void UpdateTimerText()
    {
        //if (!levelCompleted)
        //{
            int minutes = Mathf.FloorToInt(_currentTime / 60);
            int seconds = Mathf.FloorToInt(_currentTime % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //}
    }
}
