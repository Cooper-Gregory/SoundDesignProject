using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class crosshairRotatior : MonoBehaviour
{
    public Vector2 playerLookDirection;
    public Vector2 playerMoveDirection;
    public GameObject pivot;
    public void OnLook(InputAction.CallbackContext context)
    {
        playerLookDirection = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        playerMoveDirection = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        float x = playerLookDirection.x;
        float y = playerLookDirection.y;


        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(x, y, 0));
        pivot.transform.rotation = rotation;
        Debug.Log(rotation);
    }
}
