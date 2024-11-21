using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    public int cardCost;
    public CardType cardType;

    [TextArea]
    public string description;

    //将要执行的效果
    public List<Effect> effectList = new();

}