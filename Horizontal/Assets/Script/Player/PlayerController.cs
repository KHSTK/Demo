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
    [Header("��������")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        //��ʼ������
        rb = GetComponent<Rigidbody2D>();
        inputCentrol = new PlayerInputCentrol();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
        //started����ʱִ��
        inputCentrol.GamePlayer.Jump.started+=Jump;
    }
    //������
    private void OnEnable()
    {
        inputCentrol.Enable();
    }
    //���ٺ�
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

        if(physicsCheck.isGround)rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
