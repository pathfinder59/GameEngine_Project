using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMPro.TextMeshProUGUI uiExplain = null;
    void Start()
    {
     //   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            uiExplain.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            uiExplain.gameObject.SetActive(false);
        }
    }
    
}
