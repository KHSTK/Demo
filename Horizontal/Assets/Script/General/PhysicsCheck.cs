using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D collider2D;
    [Header("������")]
    public bool manual;
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
    public bool touchLeftWall;
    public bool touchRightWall;
    private void Awake()
    {
        collider2D = gameObject.GetComponent<CapsuleCollider2D>();
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
       isGround= Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset, checkRaduis,groundLayer);
        //ǽ���ж�
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }
    //���ӻ���ⷶΧ
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
