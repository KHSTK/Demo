using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public static TimeCount _instanc;
    public Text TimeText;
    public float Timenum = 90f;
    float time = 0f;
    float interval = 1f;
    private void Awake()
    {
        _instanc = this;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval&& Timenum > 0)
        {
            Timenum -= 1;
            time = 0f;
        }
        TimeText.text = Timenum.ToString();
    }
    public bool GetTime()
    {
        return Timenum!=0;
    } 
}
