using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;
    public GameObject prefab;//模板预制体
    [SerializeField] private SceneField _levelScene;//加载场景
    public LevelList levelList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //渲染关卡数据
        for (int i = 0; i < levelList.list.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(transform, false);
            go.transform.Find("image").gameObject.GetComponent<Image>().sprite = levelList.list[i].sprite;
            go.transform.Find("num").gameObject.GetComponent<TextMeshProUGUI>().text = "1 - " + levelList.list[i].levelId;
            go.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = levelList.list[i].name;

            int index = i; // 创建新的变量来捕获当前的循环变量值
            go.GetComponent<Button>().onClick.AddListener(() => StartGame(levelList.list[index]));
        }
    }

    public void StartGame(Item data)
    {
        LevelItemData.Instance.item = data;
        SceneManager.LoadScene(_levelScene);
    }

}

