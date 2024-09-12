using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    private GameObject progress;
    private GameObject head;
    private TextMeshProUGUI levelText;
    private GameObject flagPrefab;//����Ԥ����

    void Start()
    {
        progress = transform.Find("������").gameObject;
        head = transform.Find("��ʬͷ").gameObject;
        levelText = transform.Find("�ؿ��ı�").gameObject.GetComponent<TextMeshProUGUI>();
        flagPrefab = Resources.Load("Prefabs/UI/����") as GameObject;
    }

    //���ý������ͽ�ʬͷλ��
    public void SetPercent(float per)
    {
        // ͼƬ������
        progress.GetComponent<Image>().fillAmount = per;
        // ���������
        float width = progress.GetComponent<RectTransform>().sizeDelta.x;
        float rightX = width / 2;
        // ����ͷ��x��λ�ã����ұߵ�λ�� - ���������*����ֵ
        head.GetComponent<RectTransform>().localPosition = new Vector2(rightX - per * width, 0);
    }

    //��������λ��
    public void SetFlagPercent(float per)
    {
        // ���������
        float width = progress.GetComponent<RectTransform>().sizeDelta.x;
        float rightX = width / 2;
        // �����µ�����
        GameObject newFlag = Instantiate(flagPrefab);
        //false����ʾ����newFlag��������������λ�á���ת�����Ų��䣬������newFlag�ľֲ��������תֵ���Ÿ�������ı仯���ı䡣
        newFlag.transform.SetParent(transform, false);
        // ����λ��
        newFlag.GetComponent<RectTransform>().localPosition = new Vector2(rightX - per * width, 7f);
        //��Head�������丸�������еĲ㼶˳���������Ҳ������ʾ����������ͬ����������Ϸ���
        head.transform.SetAsLastSibling();
    }

    //���ùؿ��ı�
    public void SetLevelText(int per)
    {
        levelText.text = "�ؿ� " + per;
    }
}

