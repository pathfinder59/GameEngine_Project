using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        HitPoint = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.5f * Time.fixedDeltaTime, 0, 0);
    }

    public float HitPoint { get; private set; }
    public void Damage(float damageAmount)
    {
        HitPoint -= damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 14) return;
        gameObject.SetActive(false);
    }
}
