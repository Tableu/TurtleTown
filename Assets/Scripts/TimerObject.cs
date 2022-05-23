using System.Timers;
using UnityEngine;
public abstract class TimerObject : MonoBehaviour
{
    protected Timer _timer;
    protected float _interval;

    protected void Start()
    {
        _timer = new Timer();
        _timer.Elapsed += OnTimerEvent;
        _timer.Interval = _interval;
        _timer.Start();
    }

    protected abstract void OnTimerEvent(object source, ElapsedEventArgs e);
}
