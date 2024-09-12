using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction; // ����ʩ�����

    private Image constructionProgressImage; // ʩ��������ʾͼƬ

    private void Awake()
    {
        // ��ȡʩ��������ʾͼƬ���
        constructionProgressImage = transform.Find("Canvas").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        // ����ʩ��������ʾ
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
