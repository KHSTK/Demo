using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D collider2D;
    private PlayerController playerController;
    private Rigidbody2D rb;
    [Header("������")]
    public bool manual;
    public bool isPlayer;
    //�ŵ�λ�Ʋ�
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    //��ⷶΧ
    public float checkRaduis;
    //����ͼ��
    public LayerMask groundLayer;
    [Header("״̬")]
    //�Ƿ����
    public bool isGround;
    public bool onWall;
    public bool touchLeftWall;
    public bool touchRightWall;
    private void Awake()
    {
        if (isPlayer) playerController = gameObject.GetComponent<PlayerController>();
        collider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        onWall = false;
        if (!manual)
        {
            rightOffset = new Vector2((collider2D.bounds.size.x + collider2D.offset.x) / 2, collider2D.bounds.size.y/2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //��ɫ����checkRaduis��Χ�Լ�����
       isGround= Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset*transform.localScale, checkRaduis,groundLayer);
        //ǽ���ж�
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
        if(isPlayer)onWall = (touchLeftWall&&playerController.inputDirection.x<0f || touchRightWall&&playerController.inputDirection.x>0f) && rb.velocity.y<0f;
    }
    //���ӻ���ⷶΧ
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
