using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardDataSO")]
public class CardDataSO : ScriptableObject {
    public string cardName;
    public Sprite cardSprite;
    public int carCost;
    public CardType cardType;

    [TextArea]
    public string description;

    //将要执行的效果

}