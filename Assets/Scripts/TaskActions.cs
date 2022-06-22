using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActions
{
    public void AddResource(int value, Resource resource)
    {
        resource.Value += value;
    }
}