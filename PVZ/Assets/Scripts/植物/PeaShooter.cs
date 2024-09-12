using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plants
{
    public float interval; //�������
    public Transform bullet;//�ӵ�Ԥ����
    public Transform bulletPos;//�ӵ�����λ��
    public float attackDistance;//��������
    public LayerMask layerMask;//���ͼ��
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
                Shoot(); // ִ���������
            yield return new WaitForSeconds(interval); // �ȴ�interval��

        }
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(bulletPos.position, Vector2.right, attackDistance, layerMask); // ��������

        //���߼�⹥����Χ�Ƿ��е���
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
        Gizmos.DrawLine(bulletPos.position, bulletPos.position + Vector3.right * attackDistance); // ���Ƴ־û���������ʾ
    }
    void FirePea()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // �����ӵ�
        animator.SetBool("Attacking", false);

    }
    public void OnDie()
    {
        Destroy(gameObject);
    }
}
