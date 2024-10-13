using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//自动添加必须项
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PhysicsCheck))] 
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
    public float lostTime;
    public float lostTimeCounter;
    [Header("检测")]
    //中心点
    public Vector2 centerOffest;
    //检测盒尺寸
    public Vector2 checkSize;
    //检测距离
    public float checkDistance;
    //检测层
    public LayerMask attackLayer;
    [Header("状态")]
    public bool isDead;
    public bool isHurt;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState skillState;
    protected virtual void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
        currentSpeed = nomalSpeed;
        wait = false;
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
        currentState.Physicsupdate();
        if(!isHurt&&!isDead&&!wait)Move();
        //执行当前状态的FixedUpdata
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    public virtual void Move()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("PreMove")&& !anim.GetCurrentAnimatorStateInfo(0).IsName("SnailHideOver"))
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
        if (!FoundPlayer()&&lostTimeCounter>-1)
        {
            lostTimeCounter -= Time.deltaTime;
        }
    }
    //发现敌人
    public bool FoundPlayer()
    {
        //盒型检测正前方
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffest, checkSize, 0, faceDir,checkDistance,attackLayer);
    }
    //切换状态
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Skill=>skillState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffest+new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }

    #region 事件执行方法
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
        rb.velocity = new Vector2(0, rb.velocity.y);
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
#endregion
}
