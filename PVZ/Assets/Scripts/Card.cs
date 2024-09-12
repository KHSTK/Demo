using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector] public CardItem cardItem;//卡牌信息
    [HideInInspector] public bool hasUse = false;//是否使用
    [HideInInspector] public bool isMoving = false;//是否正在移动
    private GameObject darkBg;//黑图
    private Image image;
    private GameObject progressBar;//进度对象
    private float waitTime; //等待时间
    private int useSun;//需要阳光
    private GameObject prefab;//预制体
    public LayerMask layerMask;//检测图层
    private GameObject thisObject;

    void Start()
    {
        darkBg = transform.Find("背景").gameObject;
        progressBar = transform.Find("进度").gameObject;
        image = progressBar.GetComponent<Image>();
        Init();
    }

    private void Init()
    {
        if (cardItem == null)
        {
            Debug.Log("找不到卡牌数据");
            return;
        }
        darkBg.SetActive(false);
        image.fillAmount = 0;
        GetComponent<Image>().sprite = cardItem.sprite;
        gameObject.name = cardItem.name;
        waitTime = cardItem.waitTime;
        useSun = cardItem.useSun;
        prefab = cardItem.prefab;
    }

    void Update()
    {
        if (!GameManager.Instance.isStart) return;
        UpdateProgress();
        UpdateDarkBg();
    }

    void UpdateProgress()
    {
        progressBar.GetComponent<Image>().fillAmount -= 1 / waitTime * Time.deltaTime;
    }

    void UpdateDarkBg()
    {
        if (progressBar.GetComponent<Image>().fillAmount == 0 && GameManager.Instance.sunSum >= useSun)
        {
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.isStart) return;
        Debug.Log("开始拖拽");
        if (progressBar.GetComponent<Image>().fillAmount != 0 || GameManager.Instance.sunSum < useSun) return;
        thisObject = Instantiate(prefab, transform.position, Quaternion.identity);
        //关闭运行
        thisObject.GetComponent<Plants>().isOpen = false;
        //关闭碰撞
        thisObject.GetComponent<Collider2D>().enabled = false;
        //关闭动画
        thisObject.GetComponent<Animator>().enabled = false;
        if (thisObject != null)
        {
            // 设置为背景图层
            SetSortingLayer(thisObject, "BackGround");
            SpriteRenderer sr = thisObject.GetComponent<SpriteRenderer>();
            Color color = sr.color;
            color.a = 0.5f;
            sr.color = color;
        }
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("拖拽中");
        if (thisObject == null) return;
        //植物跟随鼠标
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        thisObject.transform.position = new Vector2(mousePosition.x, mousePosition.y); // 将物体位置设置为鼠标位置
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("拖拽结束");
        if (thisObject == null) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, layerMask);//鼠标“点检测”射线
        Debug.Log(hit.collider != null);
        if (hit.collider != null && hit.collider.transform.childCount == 0)
        {
            SpriteRenderer sr = thisObject.GetComponent<SpriteRenderer>();
            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
            thisObject.transform.parent = hit.transform;
            thisObject.transform.localPosition = Vector2.one;
            SetSortingLayer(thisObject, "Default");
            //开始运行
            thisObject.GetComponent<Plants>().isOpen = true;
            //开启碰撞
            thisObject.GetComponent<Collider2D>().enabled = true;
            //开启动画
            thisObject.GetComponent<Animator>().enabled = true;
            thisObject = null;
            //重置进度
            progressBar.GetComponent<Image>().fillAmount = 1;
            //扣除阳光
            GameManager.Instance.SetSunSum(-useSun);
        }
        else
        {
            Destroy(thisObject);
        }
    }
    void SetSortingLayer(GameObject obj, string layerName)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingLayerName = layerName;
            }
        }
    }
    //点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMoving) return;
        if (GameManager.Instance.isStart) return;
        if (hasUse)
        {
            ChooseCardPanel.Instance.RemoveCard(gameObject);
        }
        else
        {
            ChooseCardPanel.Instance.AddCard(gameObject);
        }
    }
}
