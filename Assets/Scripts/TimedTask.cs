using System;
using System.Timers;

[Serializable]
public class TimedTask
{
    private Action _endAction;
    private Action _tickAction;
    private Timer _timer;
    private bool _indefinite;

    public float TimerDuration
    {
        get;
        private set;
    }
    public Action OnTick
    {
        get;
        private set;
    }

    public TimedTask(Action endAction, Action tickAction, float timerDuration, float interval, bool indefinite = false)
    {
        _endAction = endAction;
        _tickAction = tickAction;
        _indefinite = indefinite;
        _timer = new Timer();
        _timer.Elapsed += OnTimerEvent;
        _timer.Interval = interval;
        _timer.Start();
        TimerDuration = timerDuration;
    }

    private void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        if (!_timer.Enabled)
        {
            return;
        }
        OnTick?.Invoke();
        _tickAction?.Invoke();
        if (!_indefinite)
        {
            TimerDuration -= (float) _timer.Interval;
            if (TimerDuration <= 0)
            {
                _timer.Elapsed -= OnTimerEvent;
                _timer.Stop();
                _timer.Dispose();
                _endAction?.Invoke();
            }
        }
    }
}

[Serializable]
public class TaskData
{
    public bool Indefinite;
    public float TimerDuration;
    public float Interval;
    public int Value;
    public Resource Resource;
}