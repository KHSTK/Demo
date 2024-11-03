using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//����������������֮ǰִ��
[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    [Header("�¼�����")]
    //ͨ������ִ��Save��Load
    public VoidEventSO saveDataEvent;

    public static DataManager instance;
    //������Ҫ�������ݵ�Character.ISaveable
    private List<ISaveable> saveableList = new List<ISaveable>();
    //�������ݱ���
    private Data saveData;
    //����Ŀ�����л�����saveableList��saveData��������ʵ�ִ浵
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        saveData = new Data();
    }
    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            Load();
        }
    }
    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
    }
    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;


    }
    public void RegisterSaveData(ISaveable saveable)
    {
        //����б���û��ע�ᵱǰ�ű���ע��
        if (!saveableList.Contains(saveable))
        {
            saveableList.Add(saveable);
        }
    }
    public void UnRegisterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }
    public void Save()
    {
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key + "    " + item.Value);
        }
    }
    public void Load()
    {
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(saveData);
        }
    }
}
