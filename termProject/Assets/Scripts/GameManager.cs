using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject Arrow;
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private PlayerModule playerModule;


    void Start()
    {
       
    }


    public void Awake()
    {
        
        /*
        for(int i = 0; i<4; ++i)
        {
            GameObject ob = Instantiate(prefab);
            ob.transform.localPosition = new Vector3((dir * 0.5f)-1, 0.8f*i+1, 0);
            dir *= -1;
        }
        */
    }

    public void Update()
    {
        Vector3 position = player.transform.localPosition;
        position.y = position.y + 0.3f;
        cam.transform.localPosition = position;
        
    }
}
