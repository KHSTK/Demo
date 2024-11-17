using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using DG.Tweening;


public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("卡牌组件")]
    public CardDataSO cardData;
    public SpriteRenderer cardSprite;
    public TextMeshPro typeText, costText, descriptionText, nameText;
    [Header("原始数据")]
    public Vector3 originalPos;
    public Quaternion originalRot;
    public int originalLayer;
    public bool isAnimating;

    private void Start()
    {
        Init(cardData);
    }

    /// <summary>
    /// 初始化卡牌数据
    /// </summary>
    /// <param name="data">卡牌数据</param>
    public void Init(CardDataSO data)
    {
        cardData = data;
        cardSprite.sprite = data.cardSprite;
        costText.text = data.carCost.ToString();
        descriptionText.text = data.description;
        nameText.text = data.cardName.ToString();
        typeText.text = data.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "技能",
            CardType.Buff => "效果",
            _ => throw new System.NotImplementedException()
        };

    }
    /// <summary>
    /// 设置卡牌的初始位置和旋转
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public void UpdatePosAndRot(Vector3 pos, Quaternion rot)
    {
        originalPos = pos;
        originalRot = rot;
        originalLayer = GetComponent<SortingGroup>().sortingOrder;
    }
    /// <summary>
    /// 卡牌被选中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isAnimating) { return; }
        GetComponent<SortingGroup>().sortingOrder = 100;
        transform.DOMove(originalPos + Vector3.up, 0.2f);
        transform.DORotateQuaternion(Quaternion.identity, 0.2f);

    }
    /// <summary>
    /// 卡牌被取消选中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isAnimating) { return; }
        RestCardTransform();
    }
    /// <summary>
    /// 卡牌归位
    /// </summary>
    public void RestCardTransform()
    {
        isAnimating = true;
        GetComponent<SortingGroup>().sortingOrder = originalLayer;
        transform.DOMove(originalPos, 0.2f);
        transform.DORotateQuaternion(originalRot, 0.2f).onComplete = () => isAnimating = false;
    }

}
