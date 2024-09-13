using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI sunSumText;
    public ProgressPanel progressPanel;
    int zombieDiedCount = 0;//������ʬ����
    public SpriteRenderer spriteRenderer;
    public GameObject cardPanel;
    public GameObject chooserCard;
    public Camera Camera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //���ñ���
        spriteRenderer.sprite = LevelItemData.Instance.item.bgSprite;

        Init();
        zombieDiedCount = 0;
        InitProgressPanel();
        StartCoroutine(StartUI());
    }
    
    IEnumerator StartUI()
    {
        yield return new WaitForSeconds(1f);
        Camera.transform.DOMove(new Vector3(Camera.transform.position.x + 310, Camera.transform.position.y, -10), 2f);
        yield return new WaitForSeconds(2f);
        cardPanel.SetActive(true);
        cardPanel.transform.DOMove(new Vector3(cardPanel.transform.position.x + 350, cardPanel.transform.position.y, 0), 0.3f);
        chooserCard.SetActive(true);
        chooserCard.transform.DOMove(new Vector3(chooserCard.transform.position.x, chooserCard.transform.position.y - 60, 0), 0.3f);
    }
    public void SatrSetPlant()
    {
        StartCoroutine(SetPlant());
    }
    IEnumerator SetPlant()
    {
        yield return new WaitForSeconds(0.3f);
        Camera.transform.DOMove(new Vector3(Camera.transform.position.x - 310, Camera.transform.position.y, -10), 2f);
        yield return new WaitForSeconds(3f);
        chooserCard.SetActive(true);
    }
    public void Init()
    {
        sunSumText.text = GameManager.Instance.sunSum.ToString();
    }
    //��ʼ��������
    public void InitProgressPanel()
    {
        //��ʼ����Ϊ0
        progressPanel.SetPercent(0);

        int count = GameManager.Instance.listData.Count;
        string progressId = GameManager.Instance.listData[0]["progressId"];
        //���������б���ȡ����λ��
        for(int i = 1; i < count; i++)
        {
            //��ȡ��ǰ�ֵ�����
            Dictionary<string, string> dic = GameManager.Instance.listData[i];
            if (progressId != dic["progressId"])
            {
                progressPanel.SetFlagPercent((float)i / count);
            }
            progressId = dic["progressId"];
        }
    }
    //���½���
    public void UpdateProgressPanel()
    {
        zombieDiedCount++;
        progressPanel.SetPercent((float)zombieDiedCount / GameManager.Instance.listData.Count);
    }

}

