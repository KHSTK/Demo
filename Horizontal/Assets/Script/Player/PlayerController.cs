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
    private PhysicsCheck physicsCheck;
    [Header("基本属性")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        //初始化变量
        rb = GetComponent<Rigidbody2D>();
        inputCentrol = new PlayerInputCentrol();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        //started按下时执行
        inputCentrol.GamePlayer.Jump.started+=Jump;
    }
    //创建后
    private void OnEnable()
    {
        inputCentrol.Enable();
    }
    //销毁后
    private void OnDisable()
    {
        inputCentrol.Disable();
    }
    private void Update()
    {
        inputDirection = inputCentrol.GamePlayer.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
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

        if(physicsCheck.isGround)rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
