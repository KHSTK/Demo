using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data 
{
    public string sceneToSave;
    //�ֵ�洢����
    public Dictionary<string, Vector3> characterPosDict = new Dictionary<string, Vector3>();
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
