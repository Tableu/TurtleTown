using UnityEngine;

public class StoreBuilding : Building
{
    [SerializeField] private Resource _resource;
    [SerializeField] private int _amount;
    public override void TaskReward(Customer customer)
    {
        _resource.Value += _amount;
    }
}
