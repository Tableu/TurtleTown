using System.Threading.Tasks;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [Header("Task Stats")]
    [SerializeField] private int _duration;
    [SerializeField] private int _interval;

    public async Task Enter(Customer customer)
    {
        if (customer == null)
        {
            return;
        }
        float startTime = Time.time;
        customer.Busy = true;
        customer.Hide();
        
        while (Time.time - startTime < _duration)
        {
            await Task.Delay(_interval);
        }
        
        customer.Busy = false;
        customer.Show();
        TaskReward(customer);
    }

    public abstract void TaskReward(Customer customer);
}