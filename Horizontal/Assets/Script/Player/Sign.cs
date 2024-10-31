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
    //获取可交互物体
    private IInteractable targetItem;
    public bool canPress;
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
        playerInput.GamePlayer.Confirm.started += OnConfirm;
    }
    private void OnDisable()
    {
        canPress = false;
    }
    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            //在范围内按键后调用交互接口
            targetItem.TriggerAction();
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
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
            //获取可交互物体的接口
            targetItem = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}
