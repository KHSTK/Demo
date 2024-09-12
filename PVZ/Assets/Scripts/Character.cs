using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;// �������ֵ
    public float currentHealth;// ��ǰ����ֵ
    [Header("�����޵�")]
    public float invulnerableDuration;// �޵г���ʱ��
    private bool invulnerable;// �Ƿ����޵�״̬

    private void Start()
    {
        currentHealth = maxHealth;// ��ʼ����ǰ����ֵΪ�������ֵ
        Debug.Log(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        Debug.Log(currentHealth);
        if (invulnerable) return;

        if (currentHealth - damage > 0)// ����۳��˺�������ֵ����0
        {
            currentHealth -= damage;// �۳��˺�ֵ
            StartCoroutine(InvulnerableTimer()); // �����޵�״̬��ʱ��
        }
        else
        {
            currentHealth = 0;
            GetComponent<IInteractable>().OnDie();
        }

    }

    private IEnumerator InvulnerableTimer()
    {
        invulnerable = true; // ��Ϊ�޵�״̬
        yield return new WaitForSeconds(invulnerableDuration); // �ȴ��޵г���ʱ��
        invulnerable = false; // ȡ���޵�״̬
    }

}

