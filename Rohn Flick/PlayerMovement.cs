using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    [SerializeField] private Rigidbody rb;
    private float horizontal;
    private float vertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = -Input.GetAxisRaw("Horizontal");
        vertical = -Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector3(horizontal * speed, 0, vertical * speed);
    }
}
