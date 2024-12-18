using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Data 
{
    public string sceneToSave;
    //字典存储坐标
    public Dictionary<string, SerializeVector3> characterPosDict = new Dictionary<string, SerializeVector3>();
    //字典储存数值
    public Dictionary<string, float> floatSaveData = new Dictionary<string, float>();
    
    //拆分场景使其可以序列化保存
    public void SaveGameScene(GameSceneSO saveScene )
    {
        //JsonUtility将Object转换为序列化
        sceneToSave = JsonUtility.ToJson(saveScene);
    }
    public  GameSceneSO GetSavedScene()
    {
        //临时so文件创建
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //反序列化
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);
        return newScene;
    }
}
public class SerializeVector3
{
    public float x, y, z;

    public SerializeVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }
    public Vector3 ToVecort3()
    {
        return new Vector3(x, y, z);
    }
}
