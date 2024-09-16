using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������ݴ洢
public class PackageLocalData
{
    //����ģʽ
    private static PackageLocalData _instance;
    public static PackageLocalData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLocalData();
            }
            return _instance;
        }
    }
    //�������ж�̬��Ϣ
    public List<PackageLocalItem> items;

    //�洢����
    public void SavePackage()
    {
        string inventoryJson = JsonUtility.ToJson(this);
        //���ݴ洢Ϊ�����ļ�
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
        PlayerPrefs.Save();
    }
    //��ȡ����
    public List<PackageLocalItem> LoadPackage()
    {
        //�����Ƿ����
        if (items != null)
        {
            return items;
        }
        //�����ļ���ȡ
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }
}
//��̬����������
[System.Serializable]
public class PackageLocalItem
{
    public string uid;//Ψһ��ʶ

    public int id;//��ʶ����һ����Ʒ

    public int num;//��Ʒ����

    public int level;//�ȼ�

    public bool isNew;//�Ƿ��»��

    public override string ToString()
    {
        return string.Format("[id]:{0} [num]:{1}", id, num);
    }
}

