using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("״̬")]
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
        if(!isHurt&&!isDead&&!wait)Move();
        //ִ�е�ǰ״̬��FixedUpdata
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
    }
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
}
