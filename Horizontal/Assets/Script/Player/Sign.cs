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
    //��ȡ�ɽ�������
    private IInteractable targetItem;
    public bool canPress;
    private void Awake()
    {
        //��ʼ״̬�ر��޷�������
        //anim = GetComponentInChildren<Animator>();
        anim = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputCentrol();
        playerInput.Enable();
    }
    private void OnEnable()
    {
        //�����豸����
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
            //�ڷ�Χ�ڰ�������ý����ӿ�
            targetItem.TriggerAction();
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }

    private void OnActionChange(object obj, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
        {
            //��������豸
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
        //���弤��SpriteRenderer�رգ����Լ���������
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTransform.localScale; 
    }
    //����ײ���ڿ��Ի���
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            //��ȡ�ɽ�������Ľӿ�
            targetItem = other.GetComponent<IInteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
    }
}
