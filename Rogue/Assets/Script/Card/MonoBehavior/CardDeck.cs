using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using System.Net.NetworkInformation;

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
    public int drawCount;//每回合抽牌数量
    public int maxCard;//手牌上限
    [Header("事件广播")]
    public IntEventSO drawCountEvent;
    public IntEventSO discardCountEvent;

    //测试用初始化
    private void Start()
    {
        InitDeck();
        drawCount = 3;
        maxCard = 6;
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
        ShuffleDeck();
    }
    [ContextMenu("测试抽卡")]
    public void TestDrawCard()
    {
        DrawCard(1);
    }
    public void NewTurnDrawCard()
    {
        DrawCard(drawCount);
    }


    public void DrawCard(int amount)
    {
        SetCardLayout(0);
        for (int i = 0; i < amount; i++)
        {
            if (discardDeck.Count != 0)
            {
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);//将弃牌堆中的卡牌重新添加到抽牌堆中
                }
                discardDeck.Clear();//清空弃牌堆
                ShuffleDeck();//洗牌
            }
            if (drawDeck.Count == 0)
            {
                InitDeck();

                ShuffleDeck();//洗牌
            }
            //获取抽牌堆最上面的数据
            CardDataSO currentCardData = drawDeck[0];
            //从抽牌堆中移除最上面的数据
            drawDeck.RemoveAt(0);
            //修改UI数据
            drawCountEvent.RaiseEvent(drawDeck.Count, this);
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
            //更新卡牌cost文本颜色
            currentCard.UpadteCardState();
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
    /// <summary>
    /// 洗牌打乱抽牌堆顺序
    /// </summary>
    private void ShuffleDeck()
    {
        discardDeck.Clear();
        //更新UI数量
        for (int i = 0; i < drawDeck.Count; i++)
        {
            //洗牌
            CardDataSO temp = drawDeck[i];
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];
            drawDeck[randomIndex] = temp;
        }
        drawCountEvent.RaiseEvent(drawDeck.Count, this);
        discardCountEvent.RaiseEvent(discardDeck.Count, this);
    }
    /// <summary>
    /// 弃牌
    /// </summary>
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;
        discardDeck.Add(card.cardData);
        handleCardObjectList.Remove(card);
        card.transform.DOScale(Vector3.zero, 0.2f).onComplete = () =>
        {
            cardManager.RecycleCardObject(card.gameObject);
            SetCardLayout(0);
        };
        discardCountEvent.RaiseEvent(discardDeck.Count, this);
    }
    /// <summary>
    /// 回合结束弃掉玩家所有卡牌
    /// </summary>
    public void OnPlayerTurnEnd()
    {
        if (handleCardObjectList.Count < maxCard) return;
        var currentCardList = handleCardObjectList;
        for (int i = 0; i < currentCardList.Count; i = 0)
        {
            DiscardCard(handleCardObjectList[0]);
            if (handleCardObjectList.Count < 1) break;
        }

        handleCardObjectList.Clear();
    }
    /// <summary>
    /// 游戏结束弃牌
    /// </summary>
    public void GameEndEvent()
    {
        var currentCardList = handleCardObjectList;
        for (int i = 0; i < currentCardList.Count; i = 0)
        {
            DiscardCard(handleCardObjectList[0]);
            if (handleCardObjectList.Count < 1) break;
        }

        handleCardObjectList.Clear();
    }

}
