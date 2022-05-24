using System;
using System.Timers;
using Newtonsoft.Json.Linq;
using Systems.Save;
using UnityEngine;
using UnityEngine.UI;

public abstract class TimeResource : TimerObject
{
    [SerializeField] private SaveData initialData;
    [SerializeField] private Text _text;
    private bool _init;
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

    private void Update()
    {
        _text.text = Value.ToString();
    }

    private new void Start()
    {
        base.Start();
#if UNITY_ANDROID
        if (UnityEngine.InputSystem.StepCounter.current != null)
        {
            Value += Mathf.RoundToInt(Multiplier * Rate * UnityEngine.InputSystem.StepCounter.current.stepCounter.ReadValue());
        }
#endif
        if (!_init)
        {
            Value = initialData.Value;
            Rate = initialData.Rate;
            _interval = initialData.Interval;
            Multiplier = initialData.Multiplier;
            _text.text = Value.ToString();
        }
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
        _interval = saveData.Interval;
        Multiplier = saveData.Multiplier;
        _init = true;
    }

    [Serializable]
    public struct SaveData
    {
        public int Value;
        public int Rate;
        public float Interval;
        public float Multiplier;
    }
}