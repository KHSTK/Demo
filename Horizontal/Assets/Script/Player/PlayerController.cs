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
    [Header("基本属性")]
    public float speed;
    public float jumpForce;
    public float hurtForce;
    [Header("物理材质")]
    public PhysicsMaterial2D nomal;
    public PhysicsMaterial2D wall;
    [Header("状态")]
    public bool isHurt;
    public bool isCrouch;
    public bool isDead;
    public bool isAttack;
    private Vector2 originalSize;
    private Vector2 originalOffset;
    private void Awake()
    {
        //初始化变量
        rb = GetComponent<Rigidbody2D>();
        inputCentrol = new PlayerInputCentrol();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        collider2D = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        originalSize = collider2D.size;
        originalOffset = collider2D.offset;
        //started按下时执行
        inputCentrol.GamePlayer.Jump.started+=Jump;
        //performed按住时执行
        inputCentrol.GamePlayer.Crouch.performed += CrouchDown;
        inputCentrol.GamePlayer.Crouch.canceled += CrouchOver;
        //攻击
        inputCentrol.GamePlayer.Attack.started += PlayerAttack;
    }



    //创建后
    private void OnEnable()
    {
        //初始化控制器
        inputCentrol.Enable();
    }
    //销毁后
    private void OnDisable()
    {
        //销毁控制器
        inputCentrol.Disable();
    }
    private void Update()
    {
        //控制器输入
        inputDirection = inputCentrol.GamePlayer.Move.ReadValue<Vector2>();
        //检测状态
        CheckState();
    }
    private void FixedUpdate()
    {
        //移动
        if(!isHurt&&!isAttack)Move();
    }
    //测试
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}

    //移动
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //人物翻转
        if (inputDirection.x > 0) spriteRenderer.flipX=false;
        if (inputDirection.x < 0) spriteRenderer.flipX =true;
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        //按下跳跃键施加力向上
        if(physicsCheck.isGround)rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CrouchDown(InputAction.CallbackContext obj)
    {
        //蹲下修改碰撞器
        isCrouch = true;
        collider2D.size = new Vector2(originalSize.x, originalSize.y * 0.75f);
        collider2D.offset = new Vector2(originalOffset.x, originalOffset.y * 0.75f);
    }
    private void CrouchOver(InputAction.CallbackContext obj)
    {
        //恢复站立修改碰撞器
        isCrouch = false;
        collider2D.size = originalSize;
        collider2D.offset = originalOffset;
    }

    //攻击
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
    }
    #region UnityEvetn
    //受伤位移
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        //取方向，normalized归一化防止力过大
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce,ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        inputCentrol.GamePlayer.Disable();
    }
    #endregion
    //检测状态
    private void CheckState()
    {
        collider2D.sharedMaterial = physicsCheck.isGround?nomal:wall;
    }
}
