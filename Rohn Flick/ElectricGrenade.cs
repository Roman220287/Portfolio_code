using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGrenade : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private AudioSource electricSFX;
    private void OnCollisionEnter(Collision collision)
    {
        Explosion();

        Destroy(gameObject, 0.5f);
    }

    private void Explosion()
    {
        electricSFX.Play();
        sphereCollider.radius = 15;

        Debug.Log("Explode");
    }
}
