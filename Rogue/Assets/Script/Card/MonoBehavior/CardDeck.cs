using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    [Header("卡牌管理器")]
    public CardManager cardManager;
    public CardLayoutManager cardLayoutManager;
    [Header("牌堆参数")]
    public List<CardDataSO> drawDeck = new();//抽牌堆
    public List<CardDataSO> discardDeck = new();//弃牌堆
    public List<Card> handleCardObjectList = new();//手牌（每回合）
    public Vector3 deckPos;//牌堆位置

    //测试用初始化
    private void Start()
    {
        InitDeck();
        DrawCard(3);
    }

    //牌堆初始化
    public void InitDeck()
    {
        drawDeck.Clear();
        foreach (var entry in cardManager.currentLibrary.cardLibraryList)
        {
            for (int i = 0; i < entry.amount; i++)
            {
                drawDeck.Add(entry.cardData);
            }
        }
        //洗牌&&更新洗牌堆和弃牌堆
    }
    [ContextMenu("测试抽卡")]
    public void TestDrawCard()
    {
        DrawCard(1);
    }


    private void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                //洗牌
            }
            //获取抽牌堆最上面的数据
            CardDataSO currentCardData = drawDeck[i];
            //从抽牌堆中移除最上面的数据
            drawDeck.RemoveAt(i);
            //获取卡牌对象
            var card = cardManager.GetCardObject().GetComponent<Card>();
            //设置卡牌位置
            card.transform.position = deckPos;
            //初始化卡牌，添加数据
            card.Init(currentCardData);
            //将卡牌对象添加到手牌列表中
            handleCardObjectList.Add(card);
            //每次抽牌后，更新手牌布局，抽牌越多间隔越长
            var delay = i * 0.2f;
            SetCardLayout(delay);
        }
    }
    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handleCardObjectList.Count; i++)
        {
            Card currentCard = handleCardObjectList[i];
            CardTransForm currentCardTransForm = cardLayoutManager.GetCardTransForm(i, handleCardObjectList.Count);
            // currentCard.transform.SetPositionAndRotation(currentCardTransForm.pos, currentCardTransForm.rot);
            currentCard.isAnimating = true;//设置卡牌正在动画中
            //卡牌缩放变为1，使用DO实现动画 
            currentCard.transform.DOScale(Vector3.one, 0.2f).SetDelay(delay).onComplete = () =>//在动画执行完成后执行的内容
            {
                //卡牌位置移动到指定位置，使用DO实现动画
                currentCard.transform.DOMove(currentCardTransForm.pos, 0.5f).onComplete = () => currentCard.isAnimating = false;
                currentCard.transform.DORotateQuaternion(currentCardTransForm.rot, 0.5f);
            };

            //卡牌排序
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            currentCard.UpdatePosAndRot(currentCardTransForm.pos, currentCardTransForm.rot);
        }
    }
}
