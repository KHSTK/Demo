using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Data 
{
    public string sceneToSave;
    //�ֵ�洢����
    public Dictionary<string, SerializeVector3> characterPosDict = new Dictionary<string, SerializeVector3>();
    //�ֵ䴢����ֵ
    public Dictionary<string, float> floatSaveData = new Dictionary<string, float>();
    
    //��ֳ���ʹ��������л�����
    public void SaveGameScene(GameSceneSO saveScene )
    {
        //JsonUtility��Objectת��Ϊ���л�
        sceneToSave = JsonUtility.ToJson(saveScene);
    }
    public  GameSceneSO GetSavedScene()
    {
        //��ʱso�ļ�����
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //�����л�
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
