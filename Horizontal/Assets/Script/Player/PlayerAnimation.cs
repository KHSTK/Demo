using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rg;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rg = gameObject.GetComponent<Rigidbody2D>();
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
        playerController = gameObject.GetComponent<PlayerController>();
    }
    private void Update()
    {
        SetAnimation();   
    }
    public void SetAnimation()
    {
        animator.SetFloat("velocityX",Mathf.Abs(rg.velocity.x));
        animator.SetFloat("velocityY",rg.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
        animator.SetBool("isCrouch", playerController.isCrouch);
        animator.SetBool("isDead", playerController.isDead);
        animator.SetBool("isAttack", playerController.isAttack);
    }
    public void PlayHurt()
    {
        animator.SetTrigger("hurt");
    }
    public void PlayAttack()
    {
        animator.SetTrigger("attack");
    }
}
