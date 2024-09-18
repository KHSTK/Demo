using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    //��Ʒ����������
    private Transform UIStars;
    private Transform UIDescription;
    private Transform UIIcon;
    private Transform UITitle;
    private Transform UILevelText;
    private Transform UISkillDescription;

    //��Ʒ��̬��Ϣ
    private PackageLocalItem packageLocalData;
    //��Ʒ��̬��Ϣ
    private PackageTableItem packageTableItem;
    //�������߼�
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
    //���Գ�ʼ��
    private void InitName()
    {
        UIStars = transform.Find("Center/Stars");
        UIDescription = transform.Find("Center/Description");
        UIIcon = transform.Find("Center/Icon");
        UITitle = transform.Find("Top/Title");
        UILevelText = transform.Find("Bottom/LevelPanel/LevelText");
        UISkillDescription = transform.Find("Bottom/SkillDescription");
    }
    //ˢ���������
    public void Refresh(PackageLocalItem packageLocalData, PackagePanel uiPanel)
    {
        //��ʼ����̬���ݡ���̬���ݡ�����Ʒ�߼�
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiPanel = uiPanel;
        //�ȼ�
        UILevelText.GetComponent<Text>().text = string.Format("Lv.{0}/40", this.packageLocalData.level.ToString());
        //����
        UIDescription.GetComponent<Text>().text = this.packageTableItem.description;
        //����
        UISkillDescription.GetComponent<Text>().text = this.packageTableItem.skillDescription;
        //��Ʒ����
        UITitle.GetComponent<Text>().text = this.packageTableItem.name;
        //ͼƬ����
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.width), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //ˢ���Ǽ�
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
