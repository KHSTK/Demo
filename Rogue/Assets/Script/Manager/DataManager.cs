using System.IO;
using Newtonsoft.Json;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    public CardLibrarySO playerCardLibrary;
    public MapLayoutSO mapLayoutSO;
    public IntVariable playerHP;
    private string savePath;
    private DataList dataList;
    private void Awake()
    {
        dataList = new DataList(mapLayoutSO, playerCardLibrary, playerHP);
        savePath = Application.persistentDataPath + "/SaveData/";
        Debug.Log(savePath);
        Load();
    }
    public void Save()
    {
        string jsonPath = savePath + "Data.sav";
        string jsonData = JsonConvert.SerializeObject(dataList);
        if (!File.Exists(jsonPath))
        {
            Directory.CreateDirectory(savePath);
        }
        File.WriteAllText(jsonPath, jsonData);
        Debug.Log("Save Data Success");
    }
    public void Load()
    {
        string jsonPath = savePath + "Data.sav";
        if (File.Exists(jsonPath))
        {
            string data = File.ReadAllText(jsonPath);
            var jsonData = JsonConvert.DeserializeObject<DataList>(data);
            dataList = jsonData;
            Debug.Log("Load Data Success");
        }
        mapLayoutSO = dataList.mapLayout;
        playerCardLibrary = dataList.cardLibrary;
        playerHP = dataList.playerHP;
    }
}
public class DataList
{
    public MapLayoutSO mapLayout;
    public CardLibrarySO cardLibrary;
    public IntVariable playerHP;
    public DataList(MapLayoutSO mapLayout, CardLibrarySO cardLibrary, IntVariable playerHP)
    {
        this.mapLayout = mapLayout;
        this.cardLibrary = cardLibrary;
        this.playerHP = playerHP;
    }
}
