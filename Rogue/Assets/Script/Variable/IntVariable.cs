using UnityEngine;
[CreateAssetMenu(menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    public int maxValue;
    public int currentValue;
    [Header("广播")]
    public IntEventSO valueChangeEvent;
    [TextArea]
    [SerializeField] private string description;
    /// <summary>
    /// 设置当前值并广播事件
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(int value)
    {
        currentValue = value;
        valueChangeEvent?.RaiseEvent(value, this);
    }
}
