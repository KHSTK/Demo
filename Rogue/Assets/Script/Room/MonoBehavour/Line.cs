using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRander;
    public float offsetSpeed=0.1f;
    public LineType lineType;
    private void Update() 
    {
        if (lineRander!=null&&lineType==LineType.Left)
        {
            //获取当前纹理偏移
            var offset=lineRander.material.mainTextureOffset;
            offset.x+=offsetSpeed*Time.deltaTime;
            lineRander.material.mainTextureOffset=offset;
        }else if (lineRander!=null&&lineType==LineType.Right){
            var offset=lineRander.material.mainTextureOffset;
            offset.x-=offsetSpeed*Time.deltaTime;
            lineRander.material.mainTextureOffset=offset;
        }
        
    }

}
