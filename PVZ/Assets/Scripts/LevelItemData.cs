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
            //在场景切换时不销毁该对象
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

