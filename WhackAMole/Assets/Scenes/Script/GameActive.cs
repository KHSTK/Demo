using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameActive : MonoBehaviour
{
    public static GameActive _instanc;
    public GameObject NextButton;
    public GameObject BackButton;
    public Text gameActive;
    public Text Score;
    public Text Pass;
    public Text TimeText;
    public int ScoreNum = 0;//初始分数
    public int PassScore;
    public static float Timenum = 90f;//倒计时
    public float whitTime = 4f;
    public float time = 0f;
    public float interval = 1f;
    private void Awake()
    {
        _instanc = this;
    }
    bool starGame;
    bool endGame;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        starGame = StarGame();
        endGame = !GetTime();
        if (time >= interval && whitTime > 0)
        {
            whitTime -= 1;
            time = 0f;
            gameActive.text = whitTime.ToString();
        }
        while (starGame)
        {
            gameActive.text = "游戏开始！";
            starGame = false;
        }
        //更新得分
        PassScore = DifficultyControl.Score;
        Score.text = ScoreNum.ToString();
        Pass.text = PassScore.ToString();
        //更新倒计时
        if (time >= interval && Timenum > 0)
        {
            Timenum -= 1;
            time = 0f;
        }
        DifficultyControl.pass = ScoreNum >= DifficultyControl.Score;
        TimeText.text = Timenum.ToString();
        if(endGame&&DifficultyControl.pass)
        {
            gameActive.text = "成功通关！";
            NextGame();
            endGame = false;
        }
        else if(endGame && !DifficultyControl.pass)
        {
            gameActive.text = "游戏结束！";
            BackStar();
        }
    }
    public void NextGame()
    {
        if(DifficultyControl.Next)
        {
            NextButton.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("GameEnd");
        }

    }
    public void BackStar()
    {
        BackButton.gameObject.SetActive(true);
    }
    //加分
    public void AddScore()
    {
        ScoreNum += 1;
    }
    //倒计时是否结束
    public bool StarGame()
    {
        return whitTime == 0;
    }
    public bool GetTime()
    {
        return Timenum != 0;
    }
}
