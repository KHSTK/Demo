using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    //脚底位移差
    public Vector2 bottomOffset;
    //检测范围
    public float checkRaduis;
    //地面图层
    public LayerMask groundLayer;
    [Header("状态")]
    //是否地面
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //角色中心checkRaduis范围以检测地面
       isGround= Physics2D.OverlapCircle((Vector2)transform.position+ bottomOffset, checkRaduis,groundLayer);
    }
    //可视化检测范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
    }
}
