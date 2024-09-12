using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [HideInInspector] public CardItem cardItem;//������Ϣ
    [HideInInspector] public bool hasUse = false;//�Ƿ�ʹ��
    [HideInInspector] public bool isMoving = false;//�Ƿ������ƶ�
    private GameObject darkBg;//��ͼ
    private Image image;
    private GameObject progressBar;//���ȶ���
    private float waitTime; //�ȴ�ʱ��
    private int useSun;//��Ҫ����
    private GameObject prefab;//Ԥ����
    public LayerMask layerMask;//���ͼ��
    private GameObject thisObject;

    void Start()
    {
        darkBg = transform.Find("����").gameObject;
        progressBar = transform.Find("����").gameObject;
        image = progressBar.GetComponent<Image>();
        Init();
    }

    private void Init()
    {
        if (cardItem == null)
        {
            Debug.Log("�Ҳ�����������");
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
    //��ʼ��ק
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.isStart) return;
        Debug.Log("��ʼ��ק");
        if (progressBar.GetComponent<Image>().fillAmount != 0 || GameManager.Instance.sunSum < useSun) return;
        thisObject = Instantiate(prefab, transform.position, Quaternion.identity);
        //�ر�����
        thisObject.GetComponent<Plants>().isOpen = false;
        //�ر���ײ
        thisObject.GetComponent<Collider2D>().enabled = false;
        //�رն���
        thisObject.GetComponent<Animator>().enabled = false;
        if (thisObject != null)
        {
            // ����Ϊ����ͼ��
            SetSortingLayer(thisObject, "BackGround");
            SpriteRenderer sr = thisObject.GetComponent<SpriteRenderer>();
            Color color = sr.color;
            color.a = 0.5f;
            sr.color = color;
        }
    }

    //��ק��
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("��ק��");
        if (thisObject == null) return;
        //ֲ��������
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        thisObject.transform.position = new Vector2(mousePosition.x, mousePosition.y); // ������λ������Ϊ���λ��
    }

    //��ק����
    public void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("��ק����");
        if (thisObject == null) return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, layerMask);//��ꡰ���⡱����
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
            //��ʼ����
            thisObject.GetComponent<Plants>().isOpen = true;
            //������ײ
            thisObject.GetComponent<Collider2D>().enabled = true;
            //��������
            thisObject.GetComponent<Animator>().enabled = true;
            thisObject = null;
            //���ý���
            progressBar.GetComponent<Image>().fillAmount = 1;
            //�۳�����
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
    //����¼�
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
