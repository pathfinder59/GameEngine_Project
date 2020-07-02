using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transforms
{
    public GameObject[] m_tTransforms;
}
public class GameManager : MonoBehaviour
{


    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject player = null;

    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private Transforms[] transforms;
    
    void Start()
    {
        for (int i = 0; i < prefabs.Length; ++i)
        {
            for (int k = 0; k < transforms[i].m_tTransforms.Length; ++k)
            {
                var go = Instantiate(prefabs[i]);
                go.transform.localPosition = transforms[i].m_tTransforms[k].transform.position;
                go.transform.localRotation = transforms[i].m_tTransforms[k].transform.rotation;
                go.transform.localScale = transforms[i].m_tTransforms[k].transform.localScale;
            }

        }

        EventHandler.Instance.Subscribe("PlayerDied", ResetPlayerPosition);
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

    public void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(-12.683f, -0.375f, 0);
    }
}
