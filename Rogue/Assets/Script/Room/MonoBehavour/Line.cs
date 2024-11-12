using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRander;
    public float offsetSpeed=0.1f;
    private void Update() 
    {
   //获取当前纹理偏移
    var offset=lineRander.material.mainTextureOffset;
    offset.x+=offsetSpeed*Time.deltaTime;
    lineRander.material.mainTextureOffset=offset;

    }

}
