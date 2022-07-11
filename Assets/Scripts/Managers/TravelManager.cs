using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Systems.Save;
using UnityEngine;

public class TravelManager : MonoBehaviour, ISavable
{
    [Serializable]
    public class TravelDestination
    {
        public float TravelDuration;
        public float RemainingDuration;
    }
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
    public TravelDestination Destination { get; private set; }
    public Action TravelStart;
    public Action TravelEnd;

    [SerializeField] private int interval;

    private void Awake()
    {
        _instance = this;
    }

    public async void StartTravel(TravelDestination destination)
    {
        Destination = destination;
        float startTime = Time.time;
        IsTravelling = true;
        TravelStart?.Invoke();
        while (Time.time - startTime < Destination.TravelDuration)
        {
            Destination.RemainingDuration = Time.time - startTime;
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
            Destination = Destination
        };
    }

    public void LoadState(JObject state)
    {
        var saveData = state.ToObject<SaveData>();
        IsTravelling = saveData.IsTravelling;
        if (saveData.Destination != null)
        {
            if (saveData.Destination.TravelDuration > 0)
            {
                StartTravel(saveData.Destination);
            }
        }
    }

    [Serializable]
    public struct SaveData
    {
        public bool IsTravelling;
        public TravelDestination Destination;
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
        
        StartTravel(new TravelDestination
        {
            TravelDuration = 100
        });
    }
#endif
}