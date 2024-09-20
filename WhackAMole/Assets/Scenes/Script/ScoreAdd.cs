using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAdd : MonoBehaviour
{
    public static ScoreAdd _instanc;
    public Text Score;
    public int ScoreNum = 0;
    private void Awake()
    {
        _instanc = this;
    }
    public void Update()
    {
        Score.text = ScoreNum.ToString();
    }
    public void AddScore()
    {
        ScoreNum += 1;
    }
}
