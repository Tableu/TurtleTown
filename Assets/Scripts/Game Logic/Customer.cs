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
    [SerializeField] private CustomerVisuals customerVisuals;
    [SerializeField] private GameObject thoughtBubble;
    [SerializeField] private Vector2 thoughtBubbleOffset;
    private GameObject _currentThoughtBubble;
    private FSM _stateMachine;
    private int _direction;
    private bool _busy;
    private int _thoughtBubbleCooldown;
    
    public bool Busy => _busy;
    public CustomerVisuals Visuals => customerVisuals;
    public float WalkingSpeed => walkingSpeed;
    public float MaxWalkingSpeed => maxWalkingSpeed;
    public int Direction => _direction;

    public void Start()
    {
        _stateMachine = new FSM();
        _stateMachine.SetState(new RandomMoveState(this));
        _thoughtBubbleCooldown = 1000;
    }

    public void FixedUpdate()
    {
        if (!_busy)
        {
            _stateMachine.Tick();

            if (_thoughtBubbleCooldown == 0)
            {
                SpawnThoughtBubble();
            }
            else if(_currentThoughtBubble == null)
            {
                _thoughtBubbleCooldown--;
            }
        }
    }

    public void Walk(int direction)
    {
        Move(direction);
        customerVisuals.FlipVisuals(direction);
        customerVisuals.Walk();
        _direction = direction;
    }

    public void Idle()
    {
        customerVisuals.Idle();
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    public void Hide()
    {
        GameUtils.SetAllRenderers(gameObject, false);
        _collider.enabled = false;
        _rigidbody.Sleep();
        _busy = true;
    }

    public void Show()
    {
        GameUtils.SetAllRenderers(gameObject, true);
        _collider.enabled = true;
        _rigidbody.WakeUp();
        _busy = false;
    }

    [ContextMenu("SpawnThoughtBubble")]
    public void SpawnThoughtBubble()
    {
        _currentThoughtBubble = Instantiate(thoughtBubble, transform);
        _currentThoughtBubble.transform.localPosition = thoughtBubbleOffset;
        _thoughtBubbleCooldown = 1000;
    }

    public async Task EnterBuilding()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Buildings"));
        if (hit && _currentThoughtBubble != null)
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
    
    private void Move(float direction)
    {
        _rigidbody.AddForce(new Vector2(WalkingSpeed*direction, 0), ForceMode2D.Impulse);
        if (Mathf.Abs(_rigidbody.velocity.x) > MaxWalkingSpeed)
        {
            _rigidbody.velocity = new Vector2(MaxWalkingSpeed * direction, _rigidbody.velocity.y);
        }
    }
}