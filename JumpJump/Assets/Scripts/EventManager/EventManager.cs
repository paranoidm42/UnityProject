using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<object>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<object>>();
        }
    }

    public static void StartListening<T>(string eventName, UnityAction<T> listener)
    {
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener((data) => listener((T)data));
        }
        else
        {
            thisEvent = new UnityEvent<object>();
            thisEvent.AddListener((data) => listener((T)data));
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening<T>(string eventName, UnityAction<T> listener)
    {
        if (eventManager == null) return;
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener((data) => listener((T)data));
        }
    }

    public static void TriggerEvent<T>(string eventName, T eventData)
    {
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventData);
        }
    }
}