using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;
    public List<CardDataSO> drawDeck=new();//抽牌堆
    public List<CardDataSO> discardDeck=new();//弃牌堆
    public List<Card> handleCardObjectList=new();//手牌（每回合）

    //测试用初始化
    private void Start() {
        InitDeck();
    }
    
    //牌堆初始化
    public void InitDeck(){
        drawDeck.Clear();
        foreach(var entry in cardManager.currentLibrary.cardLibraryList){
            for(int i = 0; i < entry.amount; i++){
                drawDeck.Add(entry.cardData);
            }
        }
        //洗牌&&更新洗牌堆和弃牌堆
    }
    [ContextMenu("测试抽卡")]
    public void TestDrawCard(){
        DrawCard(1);
    }


    private void DrawCard(int amount){
        for(int i = 0; i < amount; i++){
            if(drawDeck.Count==0){
                //洗牌
            }
            //获取抽牌堆最上面的数据
            CardDataSO currentCardData = drawDeck[i];
            //从抽牌堆中移除最上面的数据
            drawDeck.RemoveAt(i);
            //获取卡牌对象
            var card=cardManager.GetCardObject().GetComponent<Card>();
            //初始化卡牌，添加数据
            card.Init(currentCardData);
            //将卡牌对象添加到手牌列表中
            handleCardObjectList.Add(card);
        }
    }
}
