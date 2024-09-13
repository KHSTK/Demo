using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItemData : MonoBehaviour
{
    public static LevelItemData Instance;
    [HideInInspector] public Item item;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //�ڳ����л�ʱ�����ٸö���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

