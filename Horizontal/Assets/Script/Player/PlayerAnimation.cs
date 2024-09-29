using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rg;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rg = gameObject.GetComponent<Rigidbody2D>();
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
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
    }
}
