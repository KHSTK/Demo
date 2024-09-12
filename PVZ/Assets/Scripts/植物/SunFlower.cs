using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower :Plants
{
    public float interval; //�������
    public Transform sunshine;//����Ԥ����
    public float produceDistance;//��������
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Produce());
    }

    IEnumerator Produce()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval); // �ȴ�interval��
            animator.SetTrigger("Produce");//���Ŷ���
        }
    }

    //��������
    public void ProduceSunshine()
    {
        // �ڵ�λԲ�����������
        Vector2 randomPoint = Random.insideUnitCircle * produceDistance;
        // ���������λ��
        Vector3 spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0f) + transform.position;

        Instantiate(sunshine, spawnPosition, Quaternion.identity); // ��������
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
