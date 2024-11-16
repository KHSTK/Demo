using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("卡牌组件")]
    public CardDataSO cardData;
    public SpriteRenderer cardSprite;
    public TextMeshPro typeText,costText,descriptionText,nameText;
    
    private void Start() {
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
            CardType.Attack=> "攻击",
            CardType.Defense=> "技能",
            CardType.Buff=> "效果",
            _ => throw new System.NotImplementedException()
        };

    }
}
