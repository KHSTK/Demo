using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    DataDefinition GetDataID();
    //ע�ᱻ���������
    void RegisterSaveData()
    {
        DataManager.instance.RegisterSaveData(this);
    }

    //ע�������������
    void UnRegisterSaveData()
    {
        DataManager.instance.UnRegisterSaveData(this);
    }

    //��ȡ��Ҫ���������
    void GetSaveData(Data data);

    //��ȡ����
    void LoadData(Data data);
}
