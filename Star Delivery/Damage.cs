using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Fueldepletion fuelDepletion;

    private void Start()
    {
        fuelDepletion = GetComponent<Fueldepletion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
        {
            fuelDepletion.Fuel -= damage;
        }
    }
}
