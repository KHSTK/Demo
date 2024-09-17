using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class VegetableGardenManager: MonoBehaviour
{
    private float growthRate = 1f / 3600;
    private float[] vegetableProgress = new float[5];
    private float lastClosedTime;
    void Start()
    {
        //上次菜进度
        LoadvegetableProgress();
        //上次关闭游戏时间戳
        lastClosedTime = PlayerPrefs.GetFloat("lastClosedTime", Time.time); 
    }

    void Update()
    {
        //经过时间
        float elapsedTime = Time.deltaTime;
        if (lastClosedTime != 0)
        {
            //计算离开多久时间
            float timeSinceClose = Time.time - lastClosedTime;
            //离开后菜成长时间
            elapsedTime += timeSinceClose;
            lastClosedTime = 0f;
        }
        ///每颗菜进度
        for(int i = 0; i < vegetableProgress.Length; i++)
        {
            vegetableProgress[i] += growthRate * elapsedTime;
            vegetableProgress[i] = Mathf.Clamp01(vegetableProgress[i]);
        }
    }
    private void OnApplicationQuit()
    {
        //保存时间戳
        PlayerPrefs.SetFloat("lastClosedTime", Time.time);
        //保存菜
        SaveVegetableProgress();
        PlayerPrefs.Save();
    }
    //保存菜进度
    void SaveVegetableProgress()
    {
        for (int i = 0; i < vegetableProgress.Length; i++)
        {
            PlayerPrefs.SetFloat("vegetableProgress" + i, vegetableProgress[i]);
        }
    }
    //读取菜进度
    void LoadvegetableProgress()
    {
        for (int i = 0; i < vegetableProgress.Length; i++)
        {
            vegetableProgress[i]=PlayerPrefs.GetFloat("vegetableProgress" + i, 0f);
        }
    }
}
