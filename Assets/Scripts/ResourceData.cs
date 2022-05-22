using System;
using UnityEngine;

[Serializable] [CreateAssetMenu(menuName = "Data/Resource Data")]
public class ResourceData : ScriptableObject
{
    public TimeResourceData CoinData;
    public TimeResourceData EnergyData;
}
