using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("基本属性")]
    public int damage;
    public float attackRange;
    public float attackRate;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //访问被攻击的人
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
