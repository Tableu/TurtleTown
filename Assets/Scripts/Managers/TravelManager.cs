using System;
using System.Threading.Tasks;
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
    public string id => "travel_manager";
    
    public bool IsTravelling { get; private set; }
    public float RemainingDuration { get; private set; }
    public Action TravelStart;
    public Action TravelEnd;

    private void Awake()
    {
        _instance = this;
    }

    public async void StartTravel(float duration, int interval)
    {
        float startTime = Time.time;
        IsTravelling = true;
        TravelStart?.Invoke();
        while (Time.time - startTime < duration)
        {
            RemainingDuration = Time.time - startTime;
            await Task.Delay(interval);
        }
        IsTravelling = false;
        TravelEnd?.Invoke();
    }
    
    public object SaveState()
    {
        return new SaveData()
        {
            IsTravelling = IsTravelling,
            Duration = RemainingDuration
        };
    }

    public void LoadState(JObject state)
    {
        var saveData = state.ToObject<SaveData>();
        IsTravelling = saveData.IsTravelling;
        if (saveData.Duration > 0)
        {
            StartTravel(saveData.Duration, 1000);
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
        StartTravel(15, 1000);
    }
#endif
}