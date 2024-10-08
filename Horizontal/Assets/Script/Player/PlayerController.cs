using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public PlayerInputCentrol inputCentrol;
    public Vector2 inputDirection;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider2D;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    [Header("��������")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    [Header("�������")]
    public PhysicsMaterial2D nomal;
    public PhysicsMaterial2D wall;
    [Header("״̬")]
    public bool isHurt;
    public bool isCrouch;
    public bool isDead;
    public bool isAttack;
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
        originalSize = collider2D.size;
        originalOffset = collider2D.offset;
        //started����ʱִ��
        inputCentrol.GamePlayer.Jump.started+=Jump;
        //performed��סʱִ��
        inputCentrol.GamePlayer.Crouch.performed += CrouchDown;
        inputCentrol.GamePlayer.Crouch.canceled += CrouchOver;
        //����
        inputCentrol.GamePlayer.Attack.started += PlayerAttack;
    }



    //������
    private void OnEnable()
    {
        //��ʼ��������
        inputCentrol.Enable();
    }
    //���ٺ�
    private void OnDisable()
    {
        //���ٿ�����
        inputCentrol.Disable();
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
        if(!isHurt&&!isAttack)Move();
    }
    //����
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}

    //�ƶ�
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //���﷭ת
        if (inputDirection.x > 0) spriteRenderer.flipX=false;
        if (inputDirection.x < 0) spriteRenderer.flipX =true;
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        //������Ծ��ʩ��������
        if(physicsCheck.isGround)rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
        playerAnimation.PlayAttack();
        isAttack = true;
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
    }
}
