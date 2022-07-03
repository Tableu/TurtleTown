using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    public LayerMask LayerMask;
    private Vector2 dragOrigin;
    private bool dragging;

    private void Start()
    {
        PlayerInputActions playerInputActions = GlobalReferences.Instance.PlayerInputActions;
        playerInputActions.Player.Hold.performed += OnClick;
        playerInputActions.Player.Hold.canceled += OnRelease;
    }

    private void Update()
    {
        if (dragging)
        {
            Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - dragOrigin);
            Vector2 move = new Vector2(pos.x * dragSpeed, 0);
            transform.Translate(move, Space.World);
        }
    }

    private void OnDestroy()
    {
        PlayerInputActions playerInputActions = GlobalReferences.Instance.PlayerInputActions;
        playerInputActions.Player.Hold.performed -= OnClick;
        playerInputActions.Player.Hold.canceled -= OnRelease;
    }

    private void OnClick(InputAction.CallbackContext callbackContext)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask);
        if (!hit)
        {
            dragOrigin = Mouse.current.position.ReadValue();
            dragging = true;
        }
    }

    private void OnRelease(InputAction.CallbackContext callbackContext)
    {
        transform.position = Camera.main.transform.position;
        dragging = false;
    }
}
