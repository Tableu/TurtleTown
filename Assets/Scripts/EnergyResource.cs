using System.Timers;
using Systems.Save;

public class EnergyResource : TimeResource, ISavable
{
    public string id => "energy_resource";

    protected override void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        Value += Rate;
    }
}
