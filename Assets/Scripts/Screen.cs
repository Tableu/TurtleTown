using System;
using UnityEngine;

public class Screen : MonoBehaviour
{
    private static Screen _current;
    public bool init;

    public void Awake()
    {
        if (init)
        {
            _current = this;
        }
    }

    public void Show()
    {
        if (_current != this)
        {
            GameUtils.SetAllRenderers(gameObject, true);
            if (_current != null)
            {
                GameUtils.SetAllRenderers(_current.gameObject, false);
            }

            _current = this;
        }
    }
}