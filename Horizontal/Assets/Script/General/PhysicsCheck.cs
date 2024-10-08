using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D collider2D;
    [Header("检测参数")]
    public bool manual;
    //脚底位移差
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    //检测范围
    public float checkRaduis;
    //地面图层
    public LayerMask groundLayer;
    [Header("状态")]
    //是否地面
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
        //角色中心checkRaduis范围以检测地面
       isGround= Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset, checkRaduis,groundLayer);
        //墙体判断
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);
    }
    //可视化检测范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
