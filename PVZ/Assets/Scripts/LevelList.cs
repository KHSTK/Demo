using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ؿ��б�����
[CreateAssetMenu(fileName = "LevelList", menuName = "LevelList", order = 0)]
public class LevelList : ScriptableObject
{
    // �洢�������ݵ��б�
    public List<Item> list = new List<Item>();
}

// ��ʾ�����ؿ�������
[System.Serializable]
public class Item
{
    public string levelId; //�ؿ�ID
    public string name; //����
    public Sprite sprite;//ͼƬ
    public Sprite bgSprite;//����ͼƬ
}

