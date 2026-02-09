using UnityEngine;

public class SpeedControler : MonoBehaviour
{
    [SerializeField] private ShipRidgidbodyController ridgidbodyController;
    [SerializeField] private float insideSpeed;
    [SerializeField] private float outsideSpeed;

    private void Start()
    {
        outsideSpeed = ridgidbodyController.maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ridgidbodyController.maxMovementForce = insideSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        ridgidbodyController.maxMovementForce = outsideSpeed;
    }
}
