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
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/SaveData/";
        Debug.Log(savePath);
        Load();
    }
    public void Save()
    {
        string mapLayoutPath = savePath + "mapLayout.sav";
        string mapLayoutData = JsonConvert.SerializeObject(mapLayoutSO);
        if (!File.Exists(mapLayoutPath))
        {
            Directory.CreateDirectory(savePath);
        }
        File.WriteAllText(mapLayoutPath, mapLayoutData);
        Debug.Log("Save Data Success");
    }
    public void Load()
    {
        string mapLayoutPath = savePath + "mapLayout.sav";
        if (File.Exists(mapLayoutPath))
        {
            string mapLayoutData = File.ReadAllText(mapLayoutPath);
            var jsonData = JsonConvert.DeserializeObject<MapLayoutSO>(mapLayoutData);
            mapLayoutSO = jsonData;
            Debug.Log("Load Data Success");
        }
    }
}

