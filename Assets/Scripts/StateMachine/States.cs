using UnityEngine;

public class RandomMoveState : IState
{
    private Customer _customer;
    private Rigidbody2D _rigidbody2D;
    public RandomMoveState(Customer customer, Rigidbody2D rigidbody2D)
    {
        _customer = customer;
        _rigidbody2D = rigidbody2D;
    }
    public void OnEnter()
    {
        _customer.Show();
    }

    public void OnExit()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    public void Tick()
    {
        _rigidbody2D.AddForce(new Vector2(_customer.WalkingSpeed, 0), ForceMode2D.Impulse);
        if (_rigidbody2D.velocity.x > _customer.MaxWalkingSpeed)
        {
            _rigidbody2D.velocity = new Vector2(_customer.MaxWalkingSpeed * Mathf.Sign(_rigidbody2D.velocity.x), _rigidbody2D.velocity.y);
        }
    }
}