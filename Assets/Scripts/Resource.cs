using System;
using Newtonsoft.Json.Linq;
using Systems.Save;
using UnityEngine;
using UnityEngine.UI;

public abstract class Resource : MonoBehaviour, ISavable
{
    [SerializeField] private SaveData initialData;
    [SerializeField] private Text _text;
    private bool _init;
    public int Value
    {
        get; 
        set;
    }

    public abstract string id
    {
        get;
    }

    private void Update()
    {
        _text.text = Value.ToString();
    }

    private void Start()
    {
        if (!_init)
        {
            Value = initialData.Value;
            _text.text = Value.ToString();
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            Value = Value
        };
    }

    public void LoadState(JObject state)
    {
        var saveData = state.ToObject<SaveData>();
        Value = saveData.Value;
        _init = true;
    }

    [Serializable]
    public struct SaveData
    {
        public int Value;
    }
}