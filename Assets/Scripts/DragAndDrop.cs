using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public UnityEvent ItemSelected;
    public UnityEvent ItemReleased;
    public float minimumDistanceForDrag;
    public bool UseOffset;
    public Vector2 Offset;
    public LayerMask LayerMask;
    private bool dragging;
    private Vector2 originalPos;
    private GameObject target;

    private void Start()
    {
        PlayerInputActions inputAction = GlobalReferences.Instance.PlayerInputActions;
        inputAction.Player.Hold.performed += OnClick;
        inputAction.Player.Hold.canceled += OnRelease;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var dist = Vector2.Distance(originalPos, mousePos + Offset);
            if (dragging || dist >= minimumDistanceForDrag)
            {
                target.transform.position = mousePos + Offset;
                dragging = true;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerInputActions inputAction = GlobalReferences.Instance.PlayerInputActions;
        inputAction.Player.Hold.performed -= OnClick;
        inputAction.Player.Hold.canceled -= OnRelease;
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask);
        if (hit)
        {
            target = hit.collider.gameObject;
            dragging = false;
            originalPos = transform.position;
            if (UseOffset)
            {
                Offset = originalPos - (Vector2) Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }

            ItemSelected.Invoke();
        }
    }

    public void OnRelease(InputAction.CallbackContext callbackContext)
    {
        ItemReleased.Invoke();
        dragging = false;
        target = null;
    }

    public void ReturnToOriginalPosition()
    {
        if (target != null)
        {
            target.transform.position = originalPos;
        }
    }

    public void EnterBuilding()
    {
        if (target != null)
        {
            Customer customer = target.GetComponent<Customer>();
            if (customer != null)
            {
                customer.EnterBuilding();
            }
        }
    }
}
