using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System.IO;
//优于所有其他代码之前执行
[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    [Header("事件广播")]
    public VoidEventSO backToMenu;

    [Header("事件监听")]
    //通过监听执行Save和Load
    public VoidEventSO saveDataEvent;
    public VoidEventSO loadDataEvent;

    public bool isSave;
    public static DataManager instance;
    //保存需要保存数据的Character.ISaveable
    private List<ISaveable> saveableList = new List<ISaveable>();
    //保存数据本身
    private Data saveData;
    //最终目的序列化保存saveableList和saveData到本地以实现存档

    private string jsonFolder;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        saveData = new Data();
        jsonFolder = Application.persistentDataPath + "/SAVE DATA";
        ReadSaveData();
    }
    private void Update()
    {
        //if (Keyboard.current.oKey.wasPressedThisFrame)
        //{
        //    Load();
        //}
    }
    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
        loadDataEvent.OnEventRaised += Load;
    }
    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
        loadDataEvent.OnEventRaised -= Load;
    }
    public void RegisterSaveData(ISaveable saveable)
    {
        //如果列表里没有注册当前脚本再注册
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
        isSave = true;
        foreach (var saveable in saveableList)
        {
            saveable.GetSaveData(saveData);
        }
        var resultPath = jsonFolder + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveData);
        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }
        File.WriteAllText(resultPath, jsonData);

        //foreach (var item in saveData.characterPosDict)
        //{
        //    Debug.Log(item.Key + "    " + item.Value);
        //}
    }
    public void Load()
    {
        if (!isSave)
        {
            backToMenu.RaiseEvent();
        }
        foreach (var saveable in saveableList)
        {
            saveable.LoadData(saveData);
            Debug.Log("Continue");
        }
    }
    public void ReadSaveData()
    {
        var resultPath = jsonFolder + "data.sav";

        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);
            var jsonData = JsonConvert.DeserializeObject<Data>(stringData);
            saveData = jsonData;
        }
    }
}
