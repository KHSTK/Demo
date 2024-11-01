using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{
    [Header("�¼�����")]
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
    /// ������ɫ
    /// </summary>
    /// <param name="color">Ŀ����ɫ</param>
    /// <param name="duration">����ʱ��</param>
    private void OnFadeEvent(Color color,float duration,bool fadeIn)
    {
        fadeImage.DOBlendableColor(color, duration);
    }
}
