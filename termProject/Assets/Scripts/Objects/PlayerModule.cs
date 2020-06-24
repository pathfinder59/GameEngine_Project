using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerModule : MonoBehaviour
{
    /*   [Flags]
    public enum StatusFlag
    {
        isLeft = 1,
        isReverse = 2,    // 0x0010
        isMove = 4,
        isJump = 8,
        isDown = 16,
        isRoll = 32
    }
    ..
    */
    [SerializeField] private Animator animator = null;
    [SerializeField] private Rigidbody2D rootRigid = null;
    [SerializeField] private Transform bottomTransform = null;
    [SerializeField] private CircleCollider2D bottomCollider = null;
    [SerializeField] private ObjectPooler objectPooler = null;
    [SerializeField] private TMPro.TextMeshProUGUI uiHp = null;

    private bool isLeft = false;
    private bool isReverse = false;
    private bool isMove = false;
    private bool isJump = false;
    private bool isDown = false;
    private bool isRoll = false;
    private bool isArrow = false;


    private int life;
    public bool Reverse{
        get { return isReverse; }
    }
    public bool Arrow
    {
        get { return isArrow; }
    }


    private float jumpPower = 70f;
    //[NonSerialized] public StatusFlag flag = 0;

    Collider2D curCollider = null;
    float idleTime = 0f;
    float arrowCoolTime = 0f;

    public void Start()
    {
        life = 3; 
        jumpPower = 70f;
    }
    public void Awake()
    {

    }
    private void Update()
    {
        uiHp.text = "HP: " + "10";
       // TMPro.TextMeshPro textMesh = uiHp.GetComponent<TMPro.TextMeshPro>();

        if (arrowCoolTime > 0)
            arrowCoolTime -= Time.deltaTime;

         animator.SetBool("Idle2", false);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!isLeft)
            {
                isReverse = true;
                isLeft = true;
            }
            animator.SetBool("Run", true);
            isMove = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isLeft)
            {
                isReverse = true;
                isLeft = false;
            }
            animator.SetBool("Run", true);

            isMove = true;
        }
        else
        {
            isMove = false;
            animator.SetBool("Run", false);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isJump)
                animator.SetBool("Sit", true);

            isDown = true;
        }
        else
        {
            if (isDown)
                animator.SetBool("Sit", false);
            isDown = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump) return;

            animator.SetBool("Jump", true);
            //animator.SetBool("RUN", false);
            
            if (!isDown)
            {
                
                rootRigid.velocity = Vector2.zero;
                rootRigid.AddForce(new Vector2(0, jumpPower));
            }
            else
            {

                animator.SetBool("Sit", false);
                if (!curCollider)
                {
                    return;
                }
                animator.SetBool("Drop", true);
                PlatformEffector2D effector = curCollider.gameObject.GetComponent<PlatformEffector2D>();
                effector.surfaceArc = 0.0f;
            }
            isJump = true;
            bottomCollider.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (arrowCoolTime <= 0 && !isMove)
            {
                if (!isArrow)
                    isArrow = true;
                animator.SetBool("Arrow", true);

                objectPooler.Spawn("Arrow", transform);
                arrowCoolTime = 0.23f;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
         
            isArrow = false;
            animator.SetBool("Arrow", false);
        }
        if (Time.time - idleTime > 6)
        {
            animator.SetBool("Idle2", true);
            idleTime = Time.time + 2.5f;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16 || collision.gameObject.layer == 13 || collision.gameObject.layer == 12)
        {
            EventHandler.Instance.Emit("PlayerDied");
        }
        if (collision.gameObject.layer == 15)
        {
            jumpPower += 20;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 16 || collision.gameObject.layer == 13 || collision.gameObject.layer == 12)
        {
            EventHandler.Instance.Emit("PlayerDied");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.layer != 9 && collision.gameObject.layer != 11) return;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer != 8 && collision.gameObject.layer != 9 && collision.gameObject.layer != 11) return;

        isRoll = true;
        if (collision.gameObject.layer == 8)
        {
            PlatformEffector2D effector = collision.gameObject.GetComponent<PlatformEffector2D>();
            if (effector.surfaceArc == 180)
            {
                if (curCollider && collision != curCollider)
                {
                    effector = curCollider.gameObject.GetComponent<PlatformEffector2D>();
                    effector.surfaceArc = 180;
                }
                curCollider = collision;
            }
            else
            { return; }
        }
        else
        {

            if (curCollider)
            {
                PlatformEffector2D effector = curCollider.gameObject.GetComponent<PlatformEffector2D>();
                effector.surfaceArc = 180;
            }
            curCollider = null;
        }

        if (rootRigid.velocity.y <= 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Drop", false);
            isJump = false;
            rootRigid.velocity = new Vector2(rootRigid.velocity.x, 0);
        }
        else if (rootRigid.velocity.y == 0)
        {
            bottomCollider.enabled = false;
        }

    }
    
    private void FixedUpdate()
    {
        

        if (isMove)
        {
            if (isReverse)
            {
                transform.localRotation = isLeft ?
                    Quaternion.Euler(0, 180, 0) :
                    Quaternion.identity;

                isReverse = false;
            }
            transform.Translate(1f * Time.fixedDeltaTime, 0, 0);
            idleTime = Time.time;
        }

        if (rootRigid.velocity.y < 0)
        {
            if (!isJump)
            {
                isJump = true;
                animator.SetBool("Jump", true);
                animator.SetBool("Drop", true);
                bottomCollider.enabled = true;
            }
            idleTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!isJump)
            {
                float dir;
                animator.SetBool("Roll", true);
                dir = isLeft ?
                        -1 : 1;
                rootRigid.AddForce(new Vector3(2.5f * dir, 2.5f, 0));
            }
        }
        else
        { animator.SetBool("Roll", false); }

    }

}
