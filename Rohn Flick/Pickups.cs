using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int healing;
    [SerializeField] private int addArmor;

    private void OnTriggerEnter(Collider other)
    {
        //Health PickUp
        if (other.gameObject.CompareTag("Medkit"))
        {
            //if (playerHealth.health < 5)
            //{
            playerHealth.health += healing;
            Destroy(other.gameObject);
            //}
        }

        //ArmorPickUp
        if (other.gameObject.CompareTag("Armor PickUp"))
        {
            //if (playerHealth.armor < 5)
            //{
            playerHealth.armor += addArmor;
            Destroy(other.gameObject);
            //}
        }
    }
}
