using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
    private GameObject progress;
    private GameObject head;
    private TextMeshProUGUI levelText;
    private GameObject flagPrefab;//旗帜预制体

    void Start()
    {
        progress = transform.Find("进度条").gameObject;
        head = transform.Find("僵尸头").gameObject;
        levelText = transform.Find("关卡文本").gameObject.GetComponent<TextMeshProUGUI>();
        flagPrefab = Resources.Load("Prefabs/UI/旗帜") as GameObject;
    }

    //设置进度条和僵尸头位置
    public void SetPercent(float per)
    {
        // 图片进度条
        progress.GetComponent<Image>().fillAmount = per;
        // 进度条宽度
        float width = progress.GetComponent<RectTransform>().sizeDelta.x;
        float rightX = width / 2;
        // 设置头的x轴位置：最右边的位置 - 进度条宽度*进度值
        head.GetComponent<RectTransform>().localPosition = new Vector2(rightX - per * width, 0);
    }

    //设置旗帜位置
    public void SetFlagPercent(float per)
    {
        // 进度条宽度
        float width = progress.GetComponent<RectTransform>().sizeDelta.x;
        float rightX = width / 2;
        // 创建新的旗子
        GameObject newFlag = Instantiate(flagPrefab);
        //false：表示保持newFlag相对于世界坐标的位置、旋转和缩放不变，即不将newFlag的局部坐标和旋转值随着父级对象的变化而改变。
        newFlag.transform.SetParent(transform, false);
        // 设置位置
        newFlag.GetComponent<RectTransform>().localPosition = new Vector2(rightX - per * width, 7f);
        //把Head对象在其父级对象中的层级顺序置于最后，也就是显示在所有其他同级对象的最上方。
        head.transform.SetAsLastSibling();
    }

    //设置关卡文本
    public void SetLevelText(int per)
    {
        levelText.text = "关卡 " + per;
    }
}

