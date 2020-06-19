using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public GameObject[] prefabs;
    Dictionary<string, List<GameObject>> poolDictionary;

    public int bufferAmount;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i< prefabs.Length; i++)
        {
            var gameObjectList = new List<GameObject>();

            var prefab = prefabs[i];
            for(int j = 0; j< bufferAmount; j++)
            {
                var go = Instantiate(prefab);
                go.SetActive(false);
                go.transform.position = Vector3.zero;
                gameObjectList.Add(go);
            }

            poolDictionary.Add(prefab.name, gameObjectList);
        }
        
    }

    public GameObject Spawn(string gameObjectName,Transform tTransform)
    {
        var goList = poolDictionary[gameObjectName];
        var go = goList.FirstOrDefault(g => g.activeInHierarchy == false);

        if(go != null)
        {
            go.SetActive(true);
            go.transform.position = tTransform.position;
            go.transform.localRotation = tTransform.localRotation;

            var r = go.GetComponent<Rigidbody>();
            if(r != null)
            {
                r.velocity = Vector3.zero;
            }

  
        }
        else
        {
            var prefab = prefabs.FirstOrDefault(g => g.name == gameObjectName);
            go = Instantiate(prefab);
            goList.Add(go);
           
        }
        if(gameObjectName == "Trap_Arrow")
        {
            go.transform.Translate(0.13f, 0, 0);
        }
        return go;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
