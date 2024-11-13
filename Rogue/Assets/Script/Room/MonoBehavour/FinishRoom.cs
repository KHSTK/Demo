using UnityEngine;

public class FinishRoom : MonoBehaviour
{
    [Header("广播事件")]
    public ObjectEventSO loadMapEvent;
    private void OnMouseDown()
    {
        //返回地图
        Debug.Log("返回地图");
        loadMapEvent.RaiseEvent(null, this);
    }
}
