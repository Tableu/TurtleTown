using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Customer : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float maxWalkingSpeed;
    [Header("References")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject thoughtBubble;
    [SerializeField] private Vector2 thoughtBubbleOffset;
    private GameObject _currentThoughtBubble;
    private FSM _stateMachine;
    public bool Busy;
    public Animator Animator => animator;
    public float WalkingSpeed => walkingSpeed;
    public float MaxWalkingSpeed => maxWalkingSpeed;

    public void Start()
    {
        _stateMachine = new FSM();
        _stateMachine.SetState(new RandomMoveState(this, _rigidbody));
    }

    public void FixedUpdate()
    {
        _stateMachine.Tick();
    }

    public void Hide()
    {
        GameUtils.SetAllRenderers(gameObject, false);
        _collider.enabled = false;
        _rigidbody.Sleep();
    }

    public void Show()
    {
        GameUtils.SetAllRenderers(gameObject, true);
        _collider.enabled = true;
        _rigidbody.WakeUp();
    }

    [ContextMenu("SpawnThoughtBubble")]
    public void SpawnThoughtBubble()
    {
        _currentThoughtBubble = Instantiate(thoughtBubble, transform);
        _currentThoughtBubble.transform.localPosition = thoughtBubbleOffset;
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
                if (_currentThoughtBubble != null)
                {
                    Destroy(_currentThoughtBubble);
                }
                await building.Enter(this);
                Debug.Log("Exit");
            }
        }
    }
}