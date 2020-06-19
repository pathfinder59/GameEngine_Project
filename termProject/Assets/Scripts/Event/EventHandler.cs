using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void EventDelegate();
    public Dictionary<string, List<EventDelegate>> events;

    private static EventHandler _instance;
    public string testField;


    public EventDatabase eventDatabase;

    public static EventHandler Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;

        
        events = new Dictionary<string, List<EventDelegate>>();
    }

    public void Subscribe(string eventName, EventDelegate handler)
    {
        if(eventDatabase.events.Exists(match: e => e.eventId == eventName))
        {
            Debug.LogError(message: "eventName이라는 이벤트는 존재x");
            return;
        }

        if (events.ContainsKey(eventName))
        {
            List<EventDelegate> dels = events[eventName];
            if (dels == null) events[eventName] = new List<EventDelegate>();
            dels.Add(handler);
        }
        else
        {
            events.Add(eventName, new List<EventDelegate>());
            events[eventName].Add(handler);
        }
    }
    
    public void Emit(string eventName)
    {
        List<EventDelegate> dels = events[eventName];

        foreach(EventDelegate del in dels)
        {
            print(message: "33");
        }
    }
}
