using System;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    private static GlobalReferences _instance;

    public static GlobalReferences Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                Instantiate(go);
                _instance = go.AddComponent<GlobalReferences>();
            }
            return _instance;
        }
    }

    private float _timeAtLastStartup;

    public float TimeSinceLastStartup
    {
        get;
        private set;
    }

    public PlayerInputActions PlayerInputActions
    {
        get;
        private set;
    }

    private void Awake()
    {
        _instance = this;
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Enable();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void Load()
    {
        TimeSinceLastStartup = Mathf.Abs(Time.time - _timeAtLastStartup);
    }

    private void Save()
    {
        _timeAtLastStartup = Time.time;
    }
}
