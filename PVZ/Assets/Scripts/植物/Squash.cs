using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plants
{
    public int damage;
    private GameObject targetEnemy;
    private Animator animator;
    bool attack;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position + new Vector3(0, -50, 0), 200 * Time.deltaTime);
        }
    }
    //进入触发器
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            targetEnemy = other.gameObject;
            Invoke("SetAttack", 1f);
        }
    }
    private void SetAttack()
    {
        attack = true;
        animator.SetTrigger("attack");
    }
    //造成伤害
    public void SetDamage()
    {
        targetEnemy.GetComponent<Character>()?.TakeDamage(damage);
        Destroy(gameObject, 0.5f);
    }
}
