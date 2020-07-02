using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator = null;
    [SerializeField] private GameObject connectedObject = null;
    private bool isSwitched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSwitched)
        {
            if (collision.gameObject.layer == 10)
            {
                connectedObject.SetActive(false);
                animator.SetBool("Switch_Off", true);
                isSwitched = true;
            }
        }
    }
}
//.
