using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("������")]
    //�ŵ�λ�Ʋ�
    public Vector2 bottomOffset;
    //��ⷶΧ
    public float checkRaduis;
    //����ͼ��
    public LayerMask groundLayer;
    [Header("״̬")]
    //�Ƿ����
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //��ɫ����checkRaduis��Χ�Լ�����
       isGround= Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset, checkRaduis,groundLayer);
    }
    //���ӻ���ⷶΧ
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
