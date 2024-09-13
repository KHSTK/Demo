using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;//加载进度条
    [SerializeField] private SceneField _levelScene;//加载场景
    [SerializeField] private GameObject btnStart;//开始游戏按钮
    [SerializeField] private float loadingTime = 2;//加载时间
    private float curProgress;

    private void Start()
    {
        AudioManager.Instance.PlayMusic("ThemeSong");
        _loadingBar.value = 0;
        btnStart.SetActive(false);
    }
    private void Update()
    {
        curProgress += Time.deltaTime / loadingTime;
        if (curProgress >= 1)
        {
            curProgress = 1;
            btnStart.SetActive(true);
        }
        _loadingBar.value = curProgress;
    }
    //开始游戏
    public void StartGame()
    {
        //加载场景
        SceneManager.LoadScene(_levelScene);
        //清除 DOTween 库中当前正在进行的所有动画和补间
        DOTween.Clear();
    }

}
