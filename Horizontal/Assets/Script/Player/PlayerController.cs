using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("�����¼�")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO afterLoadedEvent;

    public PlayerInputCentrol inputCentrol;
    public Vector2 inputDirection;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider2D;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private Character character;
    [Header("��������")]
    public float faceDir;
    public float speed;
    public float jumpForce;
    public float wallJumpForce;
    public float hurtForce;
    public float slideDistance;
    public float slideSpeed;
    public int slidePowerCost;
    [Header("�������")]
    public PhysicsMaterial2D nomal;
    public PhysicsMaterial2D wall;
    [Header("״̬")]
    public bool isHurt;
    public bool isCrouch;
    public bool isDead;
    public bool isAttack;
    public bool isSlide;
    public bool wallJump;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    private void Awake()
    {
        //��ʼ������
        rb = GetComponent<Rigidbody2D>();
        inputCentrol = new PlayerInputCentrol();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        collider2D = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        character = GetComponent<Character>();
        originalSize = collider2D.size;
        originalOffset = collider2D.offset;
        faceDir = 1;
        wallJump = false;
        //started����ʱִ��
        inputCentrol.GamePlayer.Jump.started+=Jump;
        //performed��סʱִ��
        inputCentrol.GamePlayer.Crouch.performed += CrouchDown;
        inputCentrol.GamePlayer.Crouch.canceled += CrouchOver;
        //����
        inputCentrol.GamePlayer.Attack.started += PlayerAttack;
        inputCentrol.GamePlayer.Slide.started += Slide;

    }





    //������
    private void OnEnable()
    {
        //��ʼ��������
        inputCentrol.Enable();
        loadEventSO.LoadRequestEvent += OnLoadEvent;
        afterLoadedEvent.OnEventRaised += OnAfterLoadEvent;
    }
    //���ٺ�
    private void OnDisable()
    {
        //���ٿ�����
        inputCentrol.Disable();
        loadEventSO.LoadRequestEvent -= OnLoadEvent;
        afterLoadedEvent.OnEventRaised -= OnAfterLoadEvent;

    }



    private void Update()
    {
        //����������
        inputDirection = inputCentrol.GamePlayer.Move.ReadValue<Vector2>();
        //���״̬
        CheckState();
    }
    private void FixedUpdate()
    {
        //�ƶ�
        if(!isHurt&&!isAttack&&!isSlide)Move();
    }
    //����
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}

    //���س���ʱ�ر��ƶ����
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputCentrol.GamePlayer.Disable();
    }
    //������ɺ����ƶ����
    private void OnAfterLoadEvent()
    {
        inputCentrol.GamePlayer.Enable();
    }

    //�ƶ�
    public void Move()
    {
        if(!wallJump)
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //���﷭ת
        //if (inputDirection.x > 0) spriteRenderer.flipX=false;
        //if (inputDirection.x < 0) spriteRenderer.flipX =true;
        if (inputDirection.x > 0) faceDir = 1;
        if (inputDirection.x < 0) faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        //������Ծ��ʩ��������
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            //��ϻ���
            isSlide = false;
            StopAllCoroutines();
        }
        else if (physicsCheck.onWall)
        {
            Debug.Log("wallJump");
            rb.AddForce(new Vector2(-inputDirection.x/2, 3f) * wallJumpForce, ForceMode2D.Impulse);
            wallJump = true;
        }
    }
    private void Slide(InputAction.CallbackContext obj)
    {
        if (!isSlide&& physicsCheck.isGround&&character.currentPower>slidePowerCost)
        {
            isSlide = true;
            var targetPos = new Vector3(transform.position.x + slideDistance * transform.localScale.x, transform.position.y);
            character.invulnerableCounter = 0.5f;
            character.invulnerable = true;
            StartCoroutine(TriggerSlide(targetPos));
            GetComponent<Character>().OnSlide(slidePowerCost);
        }
    }
    private IEnumerator TriggerSlide(Vector3 target)
    {
        do
        {
            yield return null;
            //��������ʱ
            if (!physicsCheck.isGround)
            {
                break;
            }
            if (physicsCheck.touchLeftWall&& faceDir < 0f || physicsCheck.touchRightWall&& faceDir>0f)
            {
                isSlide = false;
                break;
            }
            rb.MovePosition(new Vector2(transform.position.x + slideSpeed * transform.localScale.x, transform.position.y));
        } while (MathF.Abs(target.x - transform.position.x) > 0.1f);
        isSlide = false;
        character.invulnerable = false;
    }
    private void CrouchDown(InputAction.CallbackContext obj)
    {
        //�����޸���ײ��
        isCrouch = true;
        collider2D.size = new Vector2(originalSize.x, originalSize.y * 0.75f);
        collider2D.offset = new Vector2(originalOffset.x, originalOffset.y * 0.75f);
    }
    private void CrouchOver(InputAction.CallbackContext obj)
    {
        //�ָ�վ���޸���ײ��
        isCrouch = false;
        collider2D.size = originalSize;
        collider2D.offset = originalOffset;
    }

    //����
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if (!physicsCheck.onWall)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
    }
    #region UnityEvetn
    //����λ��
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        //ȡ����normalized��һ����ֹ������
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputCentrol.GamePlayer.Disable();
    }
    #endregion
    //���״̬
    private void CheckState()
    {
        collider2D.sharedMaterial = physicsCheck.isGround?nomal:wall;
        if (physicsCheck.onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        if (wallJump && rb.velocity.y < 5)
        {
            wallJump = false;
        }
    }
}
