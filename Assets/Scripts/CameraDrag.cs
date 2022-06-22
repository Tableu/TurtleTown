using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector2 dragOrigin;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dragOrigin = Mouse.current.position.ReadValue();
            return;
        }

        if (!Mouse.current.leftButton.isPressed)
        {
            transform.position = Camera.main.transform.position;
            return;
        }

        Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - dragOrigin);
        Vector2 move = new Vector2(pos.x * dragSpeed, 0);
        transform.Translate(move, Space.World);
    }
}
