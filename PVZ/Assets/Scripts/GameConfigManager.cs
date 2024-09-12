using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏配置管理类
public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    private GameConfigData levelData;//关卡数据

    // 文本资源
    private TextAsset textAsset;

    // 初始化配置文件（txt文件 存储到内存）
    public void Init()
    {
        // 加载关卡数据
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);
    }

    // 获取关卡行数据
    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    // 根据ID获取关卡数据
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    //根据关卡id获取数据
    public List<Dictionary<string, string>> GetLevelList(string levelId)
    {
        return levelData.GetListByLevelId(levelId);
    }
}

