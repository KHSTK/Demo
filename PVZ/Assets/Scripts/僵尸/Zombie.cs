using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IInteractable
{
    public Vector3 direction; //移动方向
    public float speed;//速度
    private Animator animator;
    private Character character;
    public GameObject lostHead;//断头预制体
    private Transform head;//断头位置
    private bool isLostHead;//是否已经断头
    private float currentSpeed;
    public float healthLossPerSecond = 20f; // 掉头自动减少
    public float healthLossInterval = 0.5f; // 减少间隔
    private void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
        head = transform.Find("Head");
        currentSpeed = speed;
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
        //断头
        if (character.currentHealth <= 50 && isLostHead == false)
        {
            isLostHead = true;
            animator.SetBool("headless", true);
            head.gameObject.SetActive(true);
            Instantiate(lostHead, head.position, Quaternion.identity);
            StartCoroutine(LoseHealthOverTime());
        }

    }
    //进入触发器 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Plant")||other.CompareTag("Squash")) return;

        animator.SetBool("attack", true);
        currentSpeed = 0;
    }

    //离开触发器
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Plant")) return;
        animator.SetBool("attack", false);
        if(!isLostHead) currentSpeed = speed;
    }
    public void OnDie()
    {
        currentSpeed = 0;
        animator.SetBool("die", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
        GenerateZombies.Instance.ZombieDied(gameObject);
    }
    private IEnumerator LoseHealthOverTime()
    {
        while (character.currentHealth > 0)
        {
            yield return new WaitForSeconds(healthLossInterval);
            character.currentHealth -= healthLossPerSecond;
        }
        OnDie();
    }
}

