using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private PlayerInputCentrol playerInput;
    private Animator anim;
    public Transform playerTransform;
    public GameObject signSprite;
    private bool canPress;
    private void Awake()
    {
        //初始状态关闭无法获得组件
        //anim = GetComponentInChildren<Animator>();
        anim = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputCentrol();
        playerInput.Enable();
    }
    private void OnEnable()
    {
        //输入设备更改
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            //检测输入设备
            Debug.Log(((InputAction)obj).activeControl.device);
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device)
            {
                case Keyboard:
                    anim.Play("keyboard");
                    break;
                
            }
        }
    }

    private void Update()
    {
        //物体激活SpriteRenderer关闭，可以检测挂载物体
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTransform.localScale; 
    }
    //在碰撞体内可以互动
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = false;
        }
    }
}
