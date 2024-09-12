using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower :Plants
{
    public float interval; //攻击间隔
    public Transform sunshine;//阳光预制体
    public float produceDistance;//产生距离
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
            yield return new WaitForSeconds(interval); // 等待interval秒
            animator.SetTrigger("Produce");//播放动画
        }
    }

    //产生阳光
    public void ProduceSunshine()
    {
        // 在单位圆内生成随机点
        Vector2 randomPoint = Random.insideUnitCircle * produceDistance;
        // 将随机生成位置
        Vector3 spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0f) + transform.position;

        Instantiate(sunshine, spawnPosition, Quaternion.identity); // 生成阳光
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
