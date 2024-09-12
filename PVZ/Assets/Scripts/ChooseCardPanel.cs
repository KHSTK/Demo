using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ChooseCardPanel : MonoBehaviour
{
    public static ChooseCardPanel Instance;
    public GameObject cardPrefab;//����ģ��Ԥ����
    public CardData cardData;//���п�������
    public GameObject useCardPanel;//ѡ�еĿ��ƵĶ�����
    public List<GameObject> useCardList;//ѡ�еĿ���

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //��Ⱦ�����б�����
        for (int i = 0; i < cardData.cardItemDataList.Count; i++)
        {
            GameObject beforeCard = Instantiate(cardPrefab);
            beforeCard.transform.SetParent(transform, false);
            beforeCard.GetComponent<Card>().cardItem = cardData.cardItemDataList[i];
        }
    }

    //��ӿ���
    public void AddCard(GameObject go)
    {
        int curIndex = useCardList.Count;
        if (curIndex >= 8)
        {
            Debug.Log("�Ѿ�ѡ�еĿ�Ƭ�����������");
            return;
        }
        useCardList.Add(go);
        go.transform.SetParent(transform.root);//�����޸�Ϊ��ǰ�������ڵ���㸸����ȷ������UI�������ʾ
        Card card = go.GetComponent<Card>();
        card.isMoving = true;
        card.hasUse = true;
        Transform targetParent = useCardPanel.transform;
        // �����¿��Ƶ�Ŀ������
        int targetIndex = curIndex; // �����Ʒ����ڵ�ǰ�б��ĩβ

        // DoMove�ƶ���Ŀ��λ��
        go.transform.DOMove(useCardPanel.transform.position, 0.8f).OnComplete(
            () =>
            {
                go.transform.SetParent(targetParent, false); // ����Ϊ targetParent ���Ӷ���
                go.transform.SetSiblingIndex(targetIndex); // �ƶ���Ŀ���Ӷ����ĩβ
                card.isMoving = false;
            }
                );
    }

    //�Ƴ�����
    public void RemoveCard(GameObject go)
    {
        useCardList.Remove(go);
        go.transform.SetParent(transform.root);//�����޸�Ϊ��ǰ�������ڵ���㸸����ȷ������UI�������ʾ
        Card card = go.GetComponent<Card>();
        card.isMoving = true;
        card.hasUse = false;
        go.transform.DOMove(transform.position, 0.8f).OnComplete(
            () =>
            {
                go.transform.SetParent(transform, false);
                //�ƶ����丸������������б����ǰ��
                go.transform.SetAsFirstSibling();
                card.isMoving = false;
            }
        );
    }
}


