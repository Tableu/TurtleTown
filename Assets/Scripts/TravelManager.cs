using System;
using Newtonsoft.Json.Linq;
using Systems.Save;
using UnityEngine;

public class TravelManager : MonoBehaviour, ISavable
{
    private static TravelManager _instance;

    public static TravelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                Instantiate(go);
                _instance = go.AddComponent<TravelManager>();
            }
            return _instance;
        }
    }
    private TimedTask _travelTimer;
    public string id => "travel_manager";
    
    public bool IsTravelling { get; private set; }
    public Action TravelStart;
    public Action TravelEnd;


    private void Awake()
    {
        TravelEnd += delegate
        {
            IsTravelling = false;
        };
    }
    
    public void StartTravel(float duration, float interval)
    {
        IsTravelling = true;
        TravelStart?.Invoke();
        _travelTimer = new TimedTask(TravelEnd, null, duration, interval);
    }
    
    public object SaveState()
    {
        return new SaveData()
        {
            IsTravelling = IsTravelling,
            Duration = _travelTimer?.TimerDuration ?? 0f
        };
    }

    public void LoadState(JObject state)
    {
        var saveData = state.ToObject<SaveData>();
        IsTravelling = saveData.IsTravelling;
        if (saveData.Duration > 0)
        {
            _travelTimer = new TimedTask(TravelEnd, null, saveData.Duration, 1000);
        }
    }

    [Serializable]
    public struct SaveData
    {
        public bool IsTravelling;
        public float Duration;
    }
    
    #if UNITY_EDITOR
    public ParallaxBackground _parallaxBackground;
    [ContextMenu("Start Travel")]
    public void StartTravelTest()
    {
        TravelStart += delegate
        {
            Debug.Log("TravelStart");
            _parallaxBackground.EnableBackground = true;

        };
        TravelEnd += delegate
        {
            Debug.Log("TravelEnd");
            _parallaxBackground.EnableBackground = false;
        };
        StartTravel(5000, 1000);
    }
#endif
}