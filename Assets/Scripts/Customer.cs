using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Customer : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    public bool Busy;

    public void Hide()
    {
        GameUtils.SetAllRenderers(gameObject, false);
        _collider.enabled = false;
    }

    public void Show()
    {
        GameUtils.SetAllRenderers(gameObject, true);
        _collider.enabled = true;
    }

    public async Task EnterBuilding()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Buildings"));
        if (hit)
        {
            var building = hit.collider.gameObject.GetComponent<Building>();
            if (building != null)
            {
                await building.Enter(this);
                Debug.Log("Exit");
            }
        }
    }
}