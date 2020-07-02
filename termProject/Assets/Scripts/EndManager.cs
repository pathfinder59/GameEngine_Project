using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScene");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
