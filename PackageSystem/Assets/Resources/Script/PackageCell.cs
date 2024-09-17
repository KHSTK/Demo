using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    //定义物品各项
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;
    //动态数据
    private PackageLocalItem packageLocalData;
    //静态数据
    private PackageTableItem packageTableItem;
    //父物体
    private PackagePanel uiPanel;
    private void Awake()
    {
        //初始化
        InitUIName();
    }
    private void InitUIName()
    {
        UIIcon = transform.Find("Top/Icon");
        UIHead = transform.Find("Top/Head");
        UINew = transform.Find("Top/New");
        UISelect = transform.Find("Select");
        UILevel = transform.Find("Bottom/LevelText");
        UIStars= transform.Find("Bottom/Stars");
        UIDeleteSelect = transform.Find("DeleteSelect");

        UIDeleteSelect.gameObject.SetActive(false);
    }
    //刷新物品状态
    public void Refresh(PackageLocalItem packageLocalData,PackagePanel uiPanel)
    {
        //数据初始化
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiPanel = uiPanel;
        //等级信息
        UILevel.GetComponent<Text>().text = "Lv" + this.packageLocalData.level.ToString();
        //是否新获得
        UINew.gameObject.SetActive(this.packageLocalData.isNew);
        //武器图片
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //刷新星级
        RefreshStar();
    }
    public void RefreshStar()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform star = UIStars.GetChild(i);
            if (this.packageTableItem.star > i)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }
}
