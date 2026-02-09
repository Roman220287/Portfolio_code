using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Move : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float moveSpeed = 2f;

    public void MoveToPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}
