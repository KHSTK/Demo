using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateZombies : MonoBehaviour
{
    public static GenerateZombies Instance { get; private set; }
    public List<GameObject> curProgressZombie;//保存当前进度的敌人
    int zOrderIndex = 0;//排序


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        curProgressZombie = new List<GameObject>();
    }

    //生成僵尸
    public void TableCreateZombie()
    {
        //判断是否是最后一波敌人，如果表格中当前进度没有可以创建的敌人，及游戏胜利
        bool canCreate = false;

        //获取当前关卡数据
        GameManager.Instance.listData.ForEach(data =>
        {
            //属于当前进度的僵尸
            if (data["progressId"] == GameManager.Instance.curProgressId.ToString())
            {
                //延迟一段时间创建僵尸
                StartCoroutine(ITableCreateZombie(data));
                //代表当前进度有敌人
                canCreate = true;
            }
        });

        if (!canCreate)
        {
            StopAllCoroutines();//停止所有的携程
        //TODO: 游戏胜利处理
            Debug.Log("游戏胜利");
        }
    }

    IEnumerator ITableCreateZombie(Dictionary<string, string> levelItem)
    {
        yield return new WaitForSeconds(float.Parse(levelItem["createTime"]));

        //加载预制件：从Resources文件夹中加载，例如Zombie1
        GameObject zombiePrefab = Resources.Load("Prefabs/Enemy/Zombie" + levelItem["zombieType"]) as GameObject;

        //生成僵尸实例
        GameObject zombie = Instantiate(zombiePrefab);

        //根据配表的生成位置，找到父物体
        Transform zombieLine = transform.GetChild(int.Parse(levelItem["bornPos"]));
        zombie.transform.parent = zombieLine;
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex;
        zOrderIndex++;

        curProgressZombie.Add(zombie);
    }

    //消灭敌人
    public void ZombieDied(GameObject gameObject)
    {
        if (curProgressZombie.Contains(gameObject))
        {
            curProgressZombie.Remove(gameObject);
            //更新进度UI
            UIManager.Instance.UpdateProgressPanel();
        }
        //当前进度的僵尸全部消灭了，开启下一个进度
        if (curProgressZombie.Count == 0)
        {
            GameManager.Instance.curProgressId += 1;
            TableCreateZombie();
        }
    }
}


