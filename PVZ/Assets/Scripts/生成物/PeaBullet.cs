using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour, IInteractable
{
    public Vector3 direction; //发射方向
    public float speed;//速度
    public Transform Shooter; // 产生子弹预制件
    public float distanceThreshold = 1200f; // 最大距离
    public int damage;//伤害
    Animator animator;
    bool isFire;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, Shooter.position);
        if (distance > distanceThreshold)
        {
            Destroy(gameObject);
        }
    }
    public void OnDie()
    {
        Destroy(gameObject);
    }
    //进入触发器 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //触碰火炬树桩变为火豆,伤害翻倍
        if (other.gameObject.GetComponent<Torchwood>())
        {
            animator.SetBool("isFire", true);
            damage = damage * 2;
            isFire = true;
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character>()?.TakeDamage(damage);
            //AudioManager.Instance.PlaySFX("豌豆击中");
            //生成火焰效果
            if (isFire)
            {
                GameObject firePrefab = Resources.Load("Prefabs/Fire/Fire") as GameObject;
                GameObject go = Instantiate(firePrefab);
                go.transform.parent = other.gameObject.transform;
                go.transform.localPosition = Vector2.zero + new Vector2(0, -25);
            }
        }

    }

}
