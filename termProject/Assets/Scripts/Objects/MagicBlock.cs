using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public float coolTime;
    public float limitedTime;
    SpriteRenderer spriteRenderer;
    private float timeValue;
    private float limitedTimeValue;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        timeValue = 0;
        limitedTimeValue = 0;
        StartCoroutine("callCorutine");
    }
    void Update()
    {
        callCorutine().MoveNext();
    }
    // Update is called once per frame
    IEnumerator callCorutine()
    {
        PlatformEffector2D collider = gameObject.GetComponent<PlatformEffector2D>();

        while (true)
        {
            while (limitedTimeValue <= limitedTime)
            {
                limitedTimeValue += Time.deltaTime;
                yield return null;
            }
            limitedTimeValue = 0;

            while (spriteRenderer.color.a > 0.0f)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime);
                yield return null;
            }
            //각도 0
            collider.surfaceArc = 0;
            while (timeValue <= coolTime)
            {
                timeValue += Time.deltaTime;
                yield return null;
            }

            timeValue = 0;

            collider.surfaceArc = 180;
            while (spriteRenderer.color.a < 1.0f)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                    spriteRenderer.color.b, spriteRenderer.color.a + Time.deltaTime);
                yield return null;
            }
        }
        //yield return null;
    }
 
}
