using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slowmotion : MonoBehaviour
{
    [SerializeField] private PlayerMouseFollower movement;
    [SerializeField] private AudioSource slowmotionSFX;
    void Start()
    {
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            slowmotionSFX.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 0.5f;
            movement.playerSpeed = 10;
        }

        else
        {
            Time.timeScale = 1f;
            movement.playerSpeed = 5;
        }
    }
}
