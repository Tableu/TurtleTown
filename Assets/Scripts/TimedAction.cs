using System;
using System.Timers;

[Serializable]
public class TimedAction
{
    private Action _endAction;
    private Action _tickAction;
    private float _timerDuration;
    private Timer _timer;

    public TimedAction(Action endAction, Action tickAction, float timerDuration, float interval)
    {
        _endAction = endAction;
        _tickAction = tickAction;
        _timer = new Timer();
        _timer.Elapsed += OnTimerEvent;
        _timer.Interval = interval;
        _timer.Start();
        _timerDuration = timerDuration;
    }

    public void Restart(float duration = 0)
    {
        _timerDuration -= duration;
        if (_timerDuration <= 0)
        {
            _endAction?.Invoke();
            _timer.Stop();
        }
        _timer.Start(); 
    }

    private void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        _timerDuration--;
        if (_timerDuration <= 0)
        {
            _endAction?.Invoke();
            _timer.Stop();
        }
        else
        {
            _tickAction?.Invoke();
        }
    }
}
