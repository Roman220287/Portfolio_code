using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Customer_Line_Advance : MonoBehaviour
{
    [SerializeField] private Transform[] Line_Positions;
    [SerializeField] private GameObject[] Customers;
    [SerializeField] private GameObject DayCompleteUI;

    [SerializeField] private List<Daysettings> Day_Configs = new List<Daysettings>();
    private int Current_Day = 0;
    private int Customers_Served_Today = 0;
    private int Customers_Spawned_Today = 0;
    public int Max_Customers_This_Day = 0;

    [SerializeField] private List<GameObject> Customer_Line = new List<GameObject>();

    private IEnumerator NextDayRoutine()
    {
        DayCompleteUI.SetActive(true);
        Debug.Log("Day complete!");
        yield return new WaitForSeconds(2f);

        DayCompleteUI.SetActive(false);
        Current_Day++;

        if (Current_Day < Day_Configs.Count)
        {
            StartDay(Current_Day);
        }
        else
        {
            Debug.Log("All days complete! You win!");
        }
    }

    private void Start()
    {
        StartDay(Current_Day);
    }

    private void SpawnInitialCustomers()
    {
        int toSpawn = Mathf.Min(Line_Positions.Length, Max_Customers_This_Day - Customers_Spawned_Today);
        for (int i = 0; i < toSpawn; i++)
        {
            SpawnCustomerAtPosition(i);
            Customers_Spawned_Today++;
        }
    }

    private void SpawnCustomerAtPosition(int index)
    {
        int randomIndex = Random.Range(0, Customers.Length);
        GameObject customerObj = Instantiate(Customers[randomIndex], Line_Positions[index].position, Quaternion.identity);

        Customer customerScript = customerObj.GetComponent<Customer>();
        if (customerScript != null)
        {
            customerScript.SetLineManager(this);

            // Randomly choose request type
            bool wantsTwo = Random.value < 0.5f;
            customerScript.Request_Type = wantsTwo ? Customer.PotionRequestType.Double : Customer.PotionRequestType.Single;

            // Assign potion requests randomly from tags
            string[] potionTags = { "Health potion", "Mana potion", "Growth potion", "Poison potion"};
            customerScript.Requested_Potion1 = potionTags[Random.Range(0, potionTags.Length)];

            if (wantsTwo)
            {
                string secondPotion;
                do
                {
                    secondPotion = potionTags[Random.Range(0, potionTags.Length)];
                } while (secondPotion == customerScript.Requested_Potion1);

                customerScript.Requested_Potion2 = secondPotion;
            }
        }

        Customer_Line.Insert(index, customerObj);
    }

    private void AdvanceLine()
    {
        for (int i = 0; i < Customer_Line.Count; i++)
        {
            Customer_Move moveScript = Customer_Line[i].GetComponent<Customer_Move>();
            if (moveScript != null)
            {
                moveScript.MoveToPosition(Line_Positions[i].position);
            }
            else
            {
                // fallback if script is missing
                Customer_Line[i].transform.position = Line_Positions[i].position;
            }
        }
    }

    public void ServeFrontCustomer()
    {
        if (Customer_Line.Count == 0) return;

        GameObject frontCustomer = Customer_Line[0];
        Customer_Line.RemoveAt(0);
        
        Destroy(frontCustomer);

        Customers_Served_Today++;

        AdvanceLine();
        UpdateFrontCustomerStatus();

        // Spawn a new customer if we still need to
        if (Customers_Spawned_Today < Max_Customers_This_Day && Customer_Line.Count < Line_Positions.Length)
        {
            SpawnCustomerAtPosition(Customer_Line.Count);
            Customers_Spawned_Today++;
        }

        // All customers served
        if (Customers_Served_Today >= Max_Customers_This_Day)
        {
            StartCoroutine(NextDayRoutine());
        }
    }

    private void UpdateFrontCustomerStatus()
    {
        for (int i = 0; i < Customer_Line.Count; i++)
        {
            Collider col = Customer_Line[i].GetComponent<Collider>();
            Customer customerScript = Customer_Line[i].GetComponent<Customer>();

            if (col != null)
                col.enabled = (i == 0);

            if (customerScript != null)
            {
                customerScript.SetAsFrontCustomer(i == 0);
                customerScript.Timer = customerScript.Patience_Time;
            }
        }
    }

    private void StartDay(int dayIndex)
    {
        Customers_Served_Today = 0;
        Customers_Spawned_Today = 0;

        Max_Customers_This_Day = Day_Configs[dayIndex].Customer_Count;
        Customer_Line.Clear();

        SpawnInitialCustomers();
        UpdateFrontCustomerStatus();
    }

    public void RestartCurrentDay()
    {
        // Destroy all current customers
        foreach (var customer in Customer_Line)
        {
            if (customer != null) Destroy(customer);
        }

        Customer_Line.Clear();
        Customers_Served_Today = 0;
        Customers_Spawned_Today = 0;

        StartDay(Current_Day);
    }
}
