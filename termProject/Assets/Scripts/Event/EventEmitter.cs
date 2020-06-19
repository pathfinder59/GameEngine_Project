using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEmitter : MonoBehaviour
{

    
    [SerializeField] private string eventName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventHandler.Instance.Emit(eventName);
        }
    }
}
