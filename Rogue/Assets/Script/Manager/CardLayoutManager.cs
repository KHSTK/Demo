using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7f; //卡牌最大宽度
    public float cardSpacing = 1.5f; //卡牌间距
    [Header("弧形参数")]
    public float maxAngle = 16f;
    public float angleBetweenCards = 7f;
    public float radius = 17f;
    public Vector3 centerPoint;
    private List<Vector3> cardPos = new();
    private List<Quaternion> cardRot = new();

    private void Awake()
    {
        centerPoint = isHorizontal ? centerPoint : Vector3.up * -21f;
    }

    /// <summary>
    /// 获取卡牌位置
    /// </summary>
    /// <param name="index">第几张牌</param>
    /// <param name="totalCard">总共几张</param>
    /// <returns></returns>
    public CardTransForm GetCardTransForm(int index, int totalCard)
    {
        //计算卡牌位置
        CalculateCardPos(totalCard, isHorizontal);
        //获取指定卡牌的位置
        return new CardTransForm(cardPos[index], cardRot[index]);
    }
    /// <summary>
    /// 计算卡牌位置
    /// </summary>
    /// <param name="cardNum">手牌有几张</param>
    /// <param name="Horizontal">是否扇形排布</param>
    public void CalculateCardPos(int cardNum, bool Horizontal)
    {
        cardPos.Clear();
        cardRot.Clear();
        if (Horizontal)
        {
            float currentWidth = cardSpacing * (cardNum - 1);
            float totalWidth = Mathf.Min(maxWidth, currentWidth);
            float currentSpacing = totalWidth > 0 ? totalWidth / (cardNum - 1) : 0;
            for (int i = 0; i < cardNum; i++)
            {
                float xPos = 0 - (totalWidth / 2) + (i * currentSpacing);
                var pos = new Vector3(xPos, centerPoint.y, 0f);
                var rot = Quaternion.identity;
                cardPos.Add(pos);
                cardRot.Add(rot);
            }
        }
        else
        {
            float cardAngle = (cardNum - 1) * angleBetweenCards / 2f;
            float dynamicAngleBetweenCards = angleBetweenCards;
            if (cardNum > 1) dynamicAngleBetweenCards = 2f * maxAngle / (cardNum - 1);
            float totalAngle = Mathf.Min(maxAngle, cardAngle);
            float totalAngleBetweenCards = Mathf.Min(angleBetweenCards, dynamicAngleBetweenCards);
            for (int i = 0; i < cardNum; i++)
            {
                var angle = i * totalAngleBetweenCards;
                var pos = FanCardPos(totalAngle - angle);
                var rot = Quaternion.Euler(0, 0, totalAngle - angle);
                cardPos.Add(pos);
                cardRot.Add(rot);
            }
        }

    }
    //计算弧形排布的卡牌位置
    private Vector3 FanCardPos(float angle)
    {
        return new Vector3(
            //x方向位移
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius,
            //y方向位移
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
            centerPoint.z = 0f
        );
    }
}
