using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dayfailure : MonoBehaviour
{
    public static dayfailure Instance;

    [SerializeField] private Customer_Line_Advance Advance;
    [SerializeField] private GameObject Game_Over_Thing;

    public int failedCustomers = 0;
    public int maxFailures = 3;

    private IEnumerator RestartDayAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        ResetCurrentDay();
    }

    public void CustomerFailed()
    {
        failedCustomers++;
        Debug.Log("Customer failed! Total fails: " + failedCustomers);

        if (failedCustomers >= maxFailures)
        {
            TriggerGameOver();
        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void TriggerGameOver()
    {
        Debug.Log("GAME OVER");
        Game_Over_Thing.SetActive(true);

        StartCoroutine(RestartDayAfterDelay());
    }

    public void ResetCurrentDay()
    {
        Debug.Log("Resetting current day...");
        failedCustomers = 0;

        if (Game_Over_Thing != null)
            Game_Over_Thing.SetActive(false);

        if (Advance != null)
        {
            Advance.RestartCurrentDay();
        }
    }
}
