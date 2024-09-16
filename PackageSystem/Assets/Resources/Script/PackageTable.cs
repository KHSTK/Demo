using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PackageTable",fileName ="PackageTable")]
public class PackageTable : ScriptableObject
{
    public List<PackageTableItem> DataList = new List<PackageTableItem>();
}

//����������
[System.Serializable]
public class PackageTableItem
{
    public int id;//ID

    public int type;//����

    public int star;//�Ǽ�

    public string name;//����

    public string description;//��������

    public string skillDescription;//ϸ������

    public string imagePath;//ͼƬ�ز�·��
} 
