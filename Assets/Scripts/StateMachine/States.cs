using UnityEngine;

public class RandomMoveState : IState
{
    private Customer _customer;
    private int _ticksSinceChange;
    private int _ticksToChange;
    private bool _move;
    public RandomMoveState(Customer customer)
    {
        _customer = customer;
    }
    public void OnEnter()
    {
        _customer.Show();
        _customer.Walk((int)Mathf.Sign(Random.Range(-1, 1)));
        _ticksSinceChange = 0;
        _ticksToChange = Random.Range(100, 150);
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        if (_move)
        {
            var hit = Physics2D.Raycast(_customer.transform.position, new Vector2(_customer.Direction, 0), 1, LayerMask.GetMask("Platforms"));
            var direction = _customer.Direction;
            if (hit)
            {
                direction *= -1;

            }
            _customer.Walk(direction);
        }
        else
        {
            _customer.Idle();
        }
       
        _ticksSinceChange++;

        if (_ticksSinceChange == _ticksToChange)
        {
            _ticksSinceChange = 0;
            _ticksToChange = Random.Range(100, 150);
            if (Random.value >= 0.5)
            {
                _move = true;
            }
            else
            {
                _move = false;
            }
        }
    }
}