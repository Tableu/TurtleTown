using System.Timers;
using Systems.Save;

public class CoinsResource : TimeResource,ISavable
{
    public string id => "coins_resource";
    protected override void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        Value += Rate;
    }
}