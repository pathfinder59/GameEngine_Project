using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float playTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;

        if(playTime >= 3.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
