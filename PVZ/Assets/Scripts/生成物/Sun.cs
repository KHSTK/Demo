using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float interval; //间隔
    Animator animator;
    public int sun;
    public Transform targetObject; // 目标点
    public float moveDuration = 0.5f; // 移动持续时间
    private Vector3 targetPos;//目标位置
    public float speed;//掉落速度
    private bool isDestroy;//是否已经执行销毁计时
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
        yield return new WaitForSeconds(interval); // 等待interval秒
        animator.SetBool("Destroy", true);
        yield return new WaitForSeconds(0.5f); // 等待interval秒
        Destroy(gameObject);
    }
    void Idle()
    {
                animator.SetBool("Idle", true);
    }
    public void OnMouseDown()
    {
        Debug.Log("经过阳光");
        GameManager.Instance.SetSunSum(sun);
        // 移动阳光到目标位置
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
        Destroy(gameObject); // 抵达后摧毁
    }
    //设置目标位置
    public void SetTargetPos(Vector3 pos)
    {
        targetPos = pos;
    }
}


