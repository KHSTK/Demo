using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level Instance;
    public GameObject prefab;//ģ��Ԥ����
    [SerializeField] private SceneField _levelScene;//���س���
    public LevelList levelList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //��Ⱦ�ؿ�����
        for (int i = 0; i < levelList.list.Count; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(transform, false);
            go.transform.Find("image").gameObject.GetComponent<Image>().sprite = levelList.list[i].sprite;
            go.transform.Find("num").gameObject.GetComponent<TextMeshProUGUI>().text = "1 - " + levelList.list[i].levelId;
            go.transform.Find("name").gameObject.GetComponent<TextMeshProUGUI>().text = levelList.list[i].name;

            int index = i; // �����µı���������ǰ��ѭ������ֵ
            go.GetComponent<Button>().onClick.AddListener(() => StartGame(levelList.list[index]));
        }
    }

    public void StartGame(Item data)
    {
        LevelItemData.Instance.item = data;
        SceneManager.LoadScene(_levelScene);
    }

}

