using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IInteractable
{
    public Vector3 direction; //�ƶ�����
    public float speed;//�ٶ�
    private Animator animator;
    private Character character;
    public GameObject lostHead;//��ͷԤ����
    private Transform head;//��ͷλ��
    private bool isLostHead;//�Ƿ��Ѿ���ͷ
    private float currentSpeed;
    public float healthLossPerSecond = 20f; // ��ͷ�Զ�����
    public float healthLossInterval = 0.5f; // ���ټ��
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
        //��ͷ
        if (character.currentHealth <= 50 && isLostHead == false)
        {
            isLostHead = true;
            animator.SetBool("headless", true);
            head.gameObject.SetActive(true);
            Instantiate(lostHead, head.position, Quaternion.identity);
            StartCoroutine(LoseHealthOverTime());
        }

    }
    //���봥���� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Plant")||other.CompareTag("Squash")) return;

        animator.SetBool("attack", true);
        currentSpeed = 0;
    }

    //�뿪������
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

