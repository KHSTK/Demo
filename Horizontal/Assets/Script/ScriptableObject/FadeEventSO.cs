using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;

    /// <summary>
    /// Öð½¥±äºÚ
    /// </summary>
    /// <param name="duration">±äºÚÊ±¼ä</param>
    public void FadeIn(float duration)
    {
        RaisedEvent(Color.black, duration, true);
    }
    /// <summary>
    /// Öð½¥»Ö¸´
    /// </summary>
    /// <param name="duration">»Ö¸´Ê±¼ä</param>
    public void FadeOut(float duration)
    {
        RaisedEvent(Color.clear, duration, false);

    }
    public void RaisedEvent(Color color,float duration ,bool fadeIn )
    {
        OnEventRaised?.Invoke(color, duration, fadeIn);
    }
}
