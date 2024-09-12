using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject sunPrefab;
    public int sunSum;//阳光总数
    public int curLevelId = 1; //当前关卡
    public int curProgressId = 1;//当前进度
    public List<Dictionary<string, string>> listData;//当前关卡数据
    public bool isStart;//是否开始游戏
    public ProgressPanel progressPanel;
    public void Start()
    {
        isStart = false;
    }
    //开始游戏
    public void StartGame()
    {
        //AudioManager.Instance.PlayMusic("bgm1");
        isStart = true;
        //顶部随机位置掉落阳光
        InvokeRepeating("CreateSunDown", 10, 10);
        // 激活进度面板
        if (progressPanel != null)
        {
            progressPanel.gameObject.SetActive(true);
        }
        //生成僵尸
        GenerateZombies.Instance.TableCreateZombie();
        
    } 
    private void Awake()
    {
        Instance = this;
        //初始化配置表
        GameConfigManager.Instance.Init();
        //获取当前关卡数据
        listData = GameConfigManager.Instance.GetLevelList(curLevelId.ToString());
    }

    public void SetSunSum(int num)
    {
        sunSum += num;
        UIManager.Instance.Init();
    }
    public void CreateSunDown()
    {
        //获取左下角、右上角的世界坐标
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        float x = Random.Range(leftBottom.x+50 , rightTop.x-400);
        Vector3 bornPos = new Vector3(x, rightTop.y, 0);
        GameObject sun = Instantiate(sunPrefab, bornPos, Quaternion.identity);
        float y = Random.Range(leftBottom.y + 90, rightTop.y - 50);
        sun.GetComponent<Sun>().SetTargetPos(new Vector3(x, y, 0));
    }

}

