using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("监听事件")]
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
    [Header("基本属性")]
    public float faceDir;
    public float speed;
    public float jumpForce;
    public float wallJumpForce;
    public float hurtForce;
    public float slideDistance;
    public float slideSpeed;
    public int slidePowerCost;
    [Header("物理材质")]
    public PhysicsMaterial2D nomal;
    public PhysicsMaterial2D wall;
    [Header("状态")]
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
        //初始化变量
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
        //started按下时执行
        inputCentrol.GamePlayer.Jump.started+=Jump;
        //performed按住时执行
        inputCentrol.GamePlayer.Crouch.performed += CrouchDown;
        inputCentrol.GamePlayer.Crouch.canceled += CrouchOver;
        //攻击
        inputCentrol.GamePlayer.Attack.started += PlayerAttack;
        inputCentrol.GamePlayer.Slide.started += Slide;

    }





    //创建后
    private void OnEnable()
    {
        //初始化控制器
        inputCentrol.Enable();
        loadEventSO.LoadRequestEvent += OnLoadEvent;
        afterLoadedEvent.OnEventRaised += OnAfterLoadEvent;
    }
    //销毁后
    private void OnDisable()
    {
        //销毁控制器
        inputCentrol.Disable();
        loadEventSO.LoadRequestEvent -= OnLoadEvent;
        afterLoadedEvent.OnEventRaised -= OnAfterLoadEvent;

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
        if(!isHurt&&!isAttack&&!isSlide)Move();
    }
    //测试
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
    //}

    //加载场景时关闭移动检测
    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        inputCentrol.GamePlayer.Disable();
    }
    //加载完成后开启移动检测
    private void OnAfterLoadEvent()
    {
        inputCentrol.GamePlayer.Enable();
    }

    //移动
    public void Move()
    {
        if(!wallJump)
        rb.velocity = new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //人物翻转
        //if (inputDirection.x > 0) spriteRenderer.flipX=false;
        //if (inputDirection.x < 0) spriteRenderer.flipX =true;
        if (inputDirection.x > 0) faceDir = 1;
        if (inputDirection.x < 0) faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        //按下跳跃键施加力向上
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            //打断滑铲
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
            //滑到悬崖时
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
        if (!physicsCheck.onWall)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
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
