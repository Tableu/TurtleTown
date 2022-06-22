using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Facility : MonoBehaviour
{
    [SerializeField] protected TaskData _initData;
    protected TimedTask TimedTask;
    public List<Worker> Workers;

    protected void Start()
    {
        if (TimedTask == null)
        {
            if (_initData != null)
            {
                TimedTask = new TimedTask(TaskEnd, TaskTick, _initData.TimerDuration, _initData.Interval, _initData.Indefinite);
            }
        }
    }

    protected abstract void TaskStart();

    protected abstract void TaskEnd();

    protected abstract void TaskTick();
}