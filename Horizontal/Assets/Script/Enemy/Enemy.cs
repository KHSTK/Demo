using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [Header("基础属性")]
    public float nomalSpeed;
    public float chaseSpeed;
    [HideInInspector]public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    [Header("状态")]
    public bool isDead;
    public bool isHurt;
    private BaseState currentState;
    protected BaseState patrolState;
    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
        currentSpeed = nomalSpeed;
        wait = false;
        waitTimeCounter = waitTime;
    }
    //物体被激活时
    private void OnEnable()
    {
        //将当前状态设置为巡逻状态
        currentState = patrolState;
        //执行巡逻状态
        currentState.OnEnter(this);
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        
        //执行当前状态的Update
        currentState.LogicUpdate();
        TimeCounter();
    }
    //刚体移动代码放在FixedUpdate
    private void FixedUpdate()
    {
        if(!isHurt&&!isDead&&!wait)Move();
        //执行当前状态的FixedUpdata
        currentState.Physicsupdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
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
    public void OnTakeDamage(Transform attackerTrans)
    {
        //记录攻击者是谁
        attacker = attackerTrans;
        wait = false;
        waitTimeCounter = 0;
        //受伤面朝攻击者
        if (attackerTrans.position.x - transform.position.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        if (attackerTrans.position.x - transform.position.x < 0) transform.localScale = new Vector3(1, 1, 1);
        //受伤击退
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(-(attackerTrans.position.x - transform.position.x), 0).normalized;
        StartCoroutine(OnHurt(dir));
    }
    IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
    public void OnDead()
    {
        //修改图层避免碰撞
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;
    }
    public void DistroyAtferAnimator()
    {
        Destroy(gameObject);
    }
}
