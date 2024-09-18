using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    //物品描述详情项
    private Transform UIStars;
    private Transform UIDescription;
    private Transform UIIcon;
    private Transform UITitle;
    private Transform UILevelText;
    private Transform UISkillDescription;

    //物品动态信息
    private PackageLocalItem packageLocalData;
    //物品静态信息
    private PackageTableItem packageTableItem;
    //背包父逻辑
    private PackagePanel uiPanel;

    private void Awake()
    {
        InitName();
        Test();
    }
    private void Test()
    {
        Refresh(GameManager.Instance.GetPackageLocalDatas()[1], null);
    }
    //属性初始化
    private void InitName()
    {
        UIStars = transform.Find("Center/Stars");
        UIDescription = transform.Find("Center/Description");
        UIIcon = transform.Find("Center/Icon");
        UITitle = transform.Find("Top/Title");
        UILevelText = transform.Find("Bottom/LevelPanel/LevelText");
        UISkillDescription = transform.Find("Bottom/SkillDescription");
    }
    //刷新详情界面
    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiPanel)
    {
        //初始化动态数据、静态数据、父物品逻辑
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiPanel = uiPanel;
        //等级
        UILevelText.GetComponent<Text>().text = string.Format("Lv.{0}/40", this.packageLocalData.level.ToString());
        //简述
        UIDescription.GetComponent<Text>().text = this.packageTableItem.description;
        //详述
        UISkillDescription.GetComponent<Text>().text = this.packageTableItem.skillDescription;
        //物品名称
        UITitle.GetComponent<Text>().text = this.packageTableItem.name;
        //图片加载
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.width), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //刷新星级
        RefreshStar();
    }
    public void RefreshStar()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform stars = UIStars.GetChild(i);
            if (this.packageTableItem.star > i)
            {
                stars.gameObject.SetActive(true);
            }
            else
            {
                stars.gameObject.SetActive(false);
            }
        }
    }
}
