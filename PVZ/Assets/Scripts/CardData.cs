using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject
{
    // �洢�������ݵ��б�
    public List<CardItem> cardItemDataList = new List<CardItem>();
}

// ��ʾ�����ؿ�������
[System.Serializable]
public class CardItem
{
    public string name; //����
    public float waitTime; //�ȴ�ʱ��
    public int useSun;//��Ҫ����
    public GameObject prefab;//Ԥ����
    public Sprite sprite;//ͼƬ
}

