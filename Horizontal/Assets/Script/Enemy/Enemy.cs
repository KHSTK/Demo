using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("基础属性")]
    public float nomalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
        currentSpeed = nomalSpeed;
        waitTimeCounter = waitTime;
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        if (physicsCheck.touchLeftWall&&faceDir.x<0 || physicsCheck.touchRightWall && faceDir.x >0)
        {
            wait = true;
            anim.SetBool("walk", false);
        }
        TimeCounter();
    }
    //刚体移动代码放在FixedUpdate
    private void FixedUpdate()
    {
        Move();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    //计时器
    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                wait = false;
            }
        }
    }
}
