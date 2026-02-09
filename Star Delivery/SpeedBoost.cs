using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour , IButtonInput
{
    [SerializeField] private ShipRidgidbodyController ridgidbodyController;

    private void Start()
    {
        ridgidbodyController = GetComponent<ShipRidgidbodyController>();
    }

    public void OnButton(int button, bool State)
    {
        if (button == 3)
        {
            ridgidbodyController.maxMovementForce = 170;
        }
    }
}
