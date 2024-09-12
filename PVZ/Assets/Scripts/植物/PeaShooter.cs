using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plants
{
    public float interval; //攻击间隔
    public Transform bullet;//子弹预制体
    public Transform bulletPos;//子弹生成位置
    public float attackDistance;//攻击距离
    public LayerMask layerMask;//检测图层
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        if (!isOpen) yield return null;
        while (true)
        {
                Shoot(); // 执行射击操作
            yield return new WaitForSeconds(interval); // 等待interval秒

        }
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(bulletPos.position, Vector2.right, attackDistance, layerMask); // 发射射线

        //射线检测攻击范围是否有敌人
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {

                animator.SetBool("Attacking", true);

            }

        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(bulletPos.position, bulletPos.position + Vector3.right * attackDistance); // 绘制持久化的射线提示
    }
    void FirePea()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // 生成子弹
        animator.SetBool("Attacking", false);

    }
    public void OnDie()
    {
        Destroy(gameObject);
    }
}
