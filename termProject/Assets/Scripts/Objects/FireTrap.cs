using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    // Start is called before the first frame updat

    [SerializeField] private Animator animator = null;
    [SerializeField] private float coolTime = 0;
    private bool isFire;
    private float m_fAnimateTime;   //0.79f
    private float m_fCoolTime;

    void Start()
    {
        m_fAnimateTime = 0.0f;
        m_fCoolTime = 0.0f;
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFire)
        {
            m_fAnimateTime += Time.deltaTime;

            if(m_fAnimateTime > 0.79f)
            {
                isFire = false;
                animator.SetBool("Fire", false);
                m_fAnimateTime = 0;
            }
        }
        else
        {
            m_fCoolTime += Time.deltaTime;

            if(m_fCoolTime > coolTime)
            {
                isFire = true;
                animator.SetBool("Fire", true);
                m_fCoolTime = 0;
            }
        }
    }
}
