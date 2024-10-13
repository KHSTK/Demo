using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�Զ���ӱ�����
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PhysicsCheck))] 
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [Header("��������")]
    public float nomalSpeed;
    public float chaseSpeed;
    [HideInInspector]public float currentSpeed;
    public Vector3 faceDir;
    public Transform attacker;
    public float hurtForce;
    [Header("��ʱ��")]
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;
    [Header("���")]
    //���ĵ�
    public Vector2 centerOffest;
    //���гߴ�
    public Vector2 checkSize;
    //������
    public float checkDistance;
    //����
    public LayerMask attackLayer;
    [Header("״̬")]
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
    //���屻����ʱ
    private void OnEnable()
    {
        //����ǰ״̬����ΪѲ��״̬
        currentState = patrolState;
        //ִ��Ѳ��״̬
        currentState.OnEnter(this);
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        
        //ִ�е�ǰ״̬��Update
        currentState.LogicUpdate();
        TimeCounter();
    }
    //�����ƶ��������FixedUpdate
    private void FixedUpdate()
    {
        currentState.Physicsupdate();
        if(!isHurt&&!isDead&&!wait)Move();
        //ִ�е�ǰ״̬��FixedUpdata
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
    //��ʱ��
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
    //���ֵ���
    public bool FoundPlayer()
    {
        //���ͼ����ǰ��
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffest, checkSize, 0, faceDir,checkDistance,attackLayer);
    }
    //�л�״̬
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

    #region �¼�ִ�з���
    public void OnTakeDamage(Transform attackerTrans)
    {
        //��¼��������˭
        attacker = attackerTrans;
        wait = false;
        waitTimeCounter = 0;
        //�����泯������
        if (attackerTrans.position.x - transform.position.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        if (attackerTrans.position.x - transform.position.x < 0) transform.localScale = new Vector3(1, 1, 1);
        //���˻���
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
        //�޸�ͼ�������ײ
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
