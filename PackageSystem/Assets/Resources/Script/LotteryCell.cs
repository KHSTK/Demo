using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryCell : MonoBehaviour
{
    private Transform UIImage;
    private Transform UIStars;
    private Transform UINew;
    //��̬����
    private PackageLocalItem packageLocalItem;
    //��̬����
    private PackageTableItem packageTableItem;
    //������
    private LotteryPanel uiPanel;

    private void Awake()
    {
        //��ʼ��UI
        InitUI();
    }
    private void InitUI()
    {
        UIImage = transform.Find("Center/UIImage");
        UIStars = transform.Find("Bottom/Stars");
        UINew = transform.Find("Top/New");
        UINew.gameObject.SetActive(false);
    }
    public void Refresh(PackageLocalItem packageLocalItem,LotteryPanel uiPanel)
    {
        //���ݳ�ʼ��
        this.packageLocalItem = packageLocalItem;
        this.packageTableItem = GameManager.Instance.GetPackageItemById(this.packageLocalItem.id);
        this.uiPanel = uiPanel;
        //ˢ����Ϣ
        RefreshImage();
        //ˢ���Ǽ�
        RefreshStar();
    }
    //ˢ��ͼƬ
    private void RefreshImage()
    {
        Texture2D t = (Texture2D)Resources.Load(this.packageTableItem.imagePath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.width), new Vector2(0, 0));
        UIImage.GetComponent<Image>().sprite = temp;
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
