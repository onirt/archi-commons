using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventChannel<T> : ScriptableObject
{
    public UnityAction<T> eventChannel;

    public void TriggerEvent(T args)
    {
        eventChannel?.Invoke(args);
    }
}