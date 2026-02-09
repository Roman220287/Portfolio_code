using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSaveTest : MonoBehaviour
{
    public Timer timer;
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetFloat("finishedTime", timer._currentTime);
    }
}
