using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float interval; //���
    Animator animator;
    public int sun;
    public Transform targetObject; // Ŀ���
    public float moveDuration = 0.5f; // �ƶ�����ʱ��
    private Vector3 targetPos;//Ŀ��λ��
    public float speed;//�����ٶ�
    private bool isDestroy;//�Ƿ��Ѿ�ִ�����ټ�ʱ
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (targetPos != Vector3.zero && Vector3.Distance(targetPos, transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }
        else
        {
            if (isDestroy == false) StartCoroutine(SetDestroy());
        }
    }
    IEnumerator SetDestroy()
    {
        isDestroy = true;
        yield return new WaitForSeconds(interval); // �ȴ�interval��
        animator.SetBool("Destroy", true);
        yield return new WaitForSeconds(0.5f); // �ȴ�interval��
        Destroy(gameObject);
    }
    void Idle()
    {
                animator.SetBool("Idle", true);
    }
    public void OnMouseDown()
    {
        Debug.Log("��������");
        GameManager.Instance.SetSunSum(sun);
        // �ƶ����⵽Ŀ��λ��
        StartCoroutine(MoveToTargetPosition());
    }

    IEnumerator MoveToTargetPosition()
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = targetObject.position;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveDuration));
            yield return null;
        }
        Destroy(gameObject); // �ִ��ݻ�
    }
    //����Ŀ��λ��
    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }
}


