using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    [Header("事件监听")]
    public FadeEventSO fadeEvent;

    public Image fadeImage;

    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }
    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;

    }

    /// <summary>
    /// 渐变颜色
    /// </summary>
    /// <param name="color">目标颜色</param>
    /// <param name="duration">渐变时间</param>
    private void OnFadeEvent(Color color,float duration,bool fadeIn)
    {
        fadeImage.DOBlendableColor(color, duration);
    }
}
