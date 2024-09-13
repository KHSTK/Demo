using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;//���ؽ�����
    [SerializeField] private SceneField _levelScene;//���س���
    [SerializeField] private GameObject btnStart;//��ʼ��Ϸ��ť
    [SerializeField] private float loadingTime = 2;//����ʱ��
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
    //��ʼ��Ϸ
    public void StartGame()
    {
        //���س���
        SceneManager.LoadScene(_levelScene);
        //��� DOTween ���е�ǰ���ڽ��е����ж����Ͳ���
        DOTween.Clear();
    }

}
