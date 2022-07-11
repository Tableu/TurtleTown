using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public float Speed;
    public float MinSize;
    public float MaxSize;
    private void Start()
    {
        PlayerInputActions playerInputActions = GlobalReferences.Instance.PlayerInputActions;
        playerInputActions.Player.Zoom.performed += Zoom;
    }

    private void Zoom(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();
        
        VirtualCamera.m_Lens.OrthographicSize += Mathf.Sign(value.y)*Speed;
        if (VirtualCamera.m_Lens.OrthographicSize < MinSize)
        {
            VirtualCamera.m_Lens.OrthographicSize = MinSize;
        }
        
        if (VirtualCamera.m_Lens.OrthographicSize > MaxSize)
        {
            VirtualCamera.m_Lens.OrthographicSize = MaxSize;
        }
    }
}
