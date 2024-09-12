using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour, IInteractable
{
    public Vector3 direction; //���䷽��
    public float speed;//�ٶ�
    public Transform Shooter; // �����ӵ�Ԥ�Ƽ�
    public float distanceThreshold = 1200f; // ������
    public int damage;//�˺�
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
    //���봥���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //���������׮��Ϊ��,�˺�����
        if (other.gameObject.GetComponent<Torchwood>())
        {
            animator.SetBool("isFire", true);
            damage = damage * 2;
            isFire = true;
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character>()?.TakeDamage(damage);
            //AudioManager.Instance.PlaySFX("�㶹����");
            //���ɻ���Ч��
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
