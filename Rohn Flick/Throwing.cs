using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCooldown;
    [SerializeField] private KeyCode throwKey;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    bool readyToThrow;

    void Start()
    {
        readyToThrow = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectiles = Instantiate(objectToThrow, attackPoint.position, player.rotation);

        Rigidbody projectileRb = projectiles.GetComponent<Rigidbody>();

        Vector3 forceToAdd = player.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
