using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    DataDefinition GetDataID();
    //注册被保存的物体
    void RegisterSaveData()
    {
        DataManager.instance.RegisterSaveData(this);
    }

    //注销被保存的物体
    void UnRegisterSaveData()
    {
        DataManager.instance.UnRegisterSaveData(this);
    }

    //获取需要保存的数据
    void GetSaveData(Data data);

    //读取数据
    void LoadData(Data data);
}
