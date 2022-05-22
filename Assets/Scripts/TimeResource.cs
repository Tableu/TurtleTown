using System;
using System.Timers;
using UnityEngine;

public abstract class TimeResource
{
    private Timer _timer;
    public int Value
    {
        get;
        protected set;
    }

    public int Rate
    {
        get;
        set;
    }

    protected TimeResource(int value, int rate, float interval, float multiplier = 1)
    {
        Value = value;
        Rate = rate;
#if UNITY_ANDROID
        Value += Mathf.RoundToInt(multiplier * Rate * UnityEngine.InputSystem.StepCounter.current.stepCounter.ReadValue());
#endif
        _timer = new Timer();
        _timer.Elapsed += OnTimerEvent;
        _timer.Interval = interval;
        _timer.Start();
    }

    private void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        Value += Rate;
    }
}
[Serializable]
public struct TimeResourceData
{
    public int Value;
    public int Rate;
    public float Interval;
    public float Multiplier;
}
public class Coins : TimeResource
{
    public Coins(int value, int rate, float interval, float multiplier = 1) : base(value, rate, interval, multiplier)
    {
        
    }
}

public class Energy : TimeResource
{
    
    public Energy(int value, int rate, float interval, float multiplier = 1) : base(value, rate, interval, multiplier)
    {
        
    }
}