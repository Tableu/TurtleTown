using System;
using System.Timers;
using Newtonsoft.Json.Linq;
using Systems.Save;
using UnityEngine;

public abstract class TimeResource : TimerObject, ISavable
{
    public float Multiplier
    {
        get;
        set;
    } = 1;
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

    public string id => "time_resource";

    private new void Start()
    {
        base.Start();
#if UNITY_ANDROID
        Value += Mathf.RoundToInt(Multiplier * Rate * UnityEngine.InputSystem.StepCounter.current.stepCounter.ReadValue());
#endif
    }

    public object SaveState()
    {
        return new SaveData()
        {
            Value = Value,
            Rate = Rate,
            Interval = (float) _timer.Interval,
            Multiplier = Multiplier
        };
    }

    public void LoadState(JObject state)
    {
        var saveData = state.ToObject<SaveData>();
        Value = saveData.Value;
        Rate = saveData.Rate;
        _timer.Interval = saveData.Interval;
        Multiplier = saveData.Multiplier;
    }

    [Serializable]
    private struct SaveData
    {
        public int Value;
        public int Rate;
        public float Interval;
        public float Multiplier;
    }
}

public class Coins : TimeResource
{
    protected override void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        Value += Rate;
    }
}

public class Energy : TimeResource
{
    protected override void OnTimerEvent(object source, ElapsedEventArgs e)
    {
        Value += Rate;
    }
}