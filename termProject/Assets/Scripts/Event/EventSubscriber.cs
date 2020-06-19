using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    [SerializeField] private string eventName;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.Instance.Subscribe(eventName, Del);
    }

    void Del()
    {
        print(message: "Event Invoked.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
