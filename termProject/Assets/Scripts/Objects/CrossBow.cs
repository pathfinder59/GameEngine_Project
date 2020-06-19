using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{

    [SerializeField] private ObjectPooler objectPooler = null;
    [SerializeField] private float coolTime = 0;
    private float m_fCoolTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_fCoolTime = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fCoolTime > 0)
            m_fCoolTime -= Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 10) return;

        if (m_fCoolTime <= 0)
        {
            objectPooler.Spawn("Trap_Arrow", transform);
            m_fCoolTime = coolTime;
        }
    }
}