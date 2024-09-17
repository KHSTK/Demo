using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    //������Ʒ����
    private Transform UIIcon;
    private Transform UIHead;
    private Transform UINew;
    private Transform UISelect;
    private Transform UILevel;
    private Transform UIStars;
    private Transform UIDeleteSelect;
    //��̬����
    private PackageLocalItem packageLocalData;
    //��̬����
    private PackageTableItem packageTableItem;
    //������
    private PackagePanel uiPanel;
    private void Awake()
    {
        //��ʼ��
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
    //ˢ����Ʒ״̬
    public void Refresh(PackageLocalItem packageLocalData,PackagePanel uiPanel)
    {
        //���ݳ�ʼ��
        this.packageLocalData = packageLocalData;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(packageLocalData.id);
        this.uiPanel = uiPanel;
        //�ȼ���Ϣ
        UILevel.GetComponent<Text>().text = "Lv" + this.packageLocalData.level.ToString();
        //�Ƿ��»��
        UINew.gameObject.SetActive(this.packageLocalData.isNew);
        //����ͼƬ
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = temp;
        //ˢ���Ǽ�
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
