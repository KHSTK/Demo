using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ϸ���ù�����
public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();

    private GameConfigData levelData;//�ؿ�����

    // �ı���Դ
    private TextAsset textAsset;

    // ��ʼ�������ļ���txt�ļ� �洢���ڴ棩
    public void Init()
    {
        // ���عؿ�����
        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);
    }

    // ��ȡ�ؿ�������
    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    // ����ID��ȡ�ؿ�����
    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    //���ݹؿ�id��ȡ����
    public List<Dictionary<string, string>> GetLevelList(string levelId)
    {
        return levelData.GetListByLevelId(levelId);
    }
}

