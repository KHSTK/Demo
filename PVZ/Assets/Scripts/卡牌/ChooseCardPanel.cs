using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ChooseCardPanel : MonoBehaviour
{
    public static ChooseCardPanel Instance;
    public GameObject cardPrefab;//卡牌模板预制体
    public CardData cardData;//所有卡牌数据
    public GameObject useCardPanel;//选中的卡牌的对象父类
    public List<GameObject> useCardList;//选中的卡牌

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //渲染卡牌列表数据
        for (int i = 0; i < cardData.cardItemDataList.Count; i++)
        {
            GameObject beforeCard = Instantiate(cardPrefab);
            beforeCard.transform.SetParent(transform, false);
            beforeCard.GetComponent<Card>().cardItem = cardData.cardItemDataList[i];
        }
    }

    //添加卡牌
    public void AddCard(GameObject go)
    {
        int curIndex = useCardList.Count;
        if (curIndex >= 8)
        {
            Debug.Log("已经选中的卡片超过最大数量");
            return;
        }
        useCardList.Add(go);
        go.transform.SetParent(transform.root);//父级修改为当前对象所在的最顶层父对象，确保卡牌UI在最顶层显示
        Card card = go.GetComponent<Card>();
        card.isMoving = true;
        card.hasUse = true;
        Transform targetParent = useCardPanel.transform;
        // 计算新卡牌的目标索引
        int targetIndex = curIndex; // 将卡牌放置在当前列表的末尾

        // DoMove移动到目标位置
        go.transform.DOMove(useCardPanel.transform.position, 0.8f).OnComplete(
            () =>
            {
                go.transform.SetParent(targetParent, false); // 设置为 targetParent 的子对象
                go.transform.SetSiblingIndex(targetIndex); // 移动到目标子对象的末尾
                card.isMoving = false;
            }
                );
    }

    //移除卡牌
    public void RemoveCard(GameObject go)
    {
        useCardList.Remove(go);
        go.transform.SetParent(transform.root);//父级修改为当前对象所在的最顶层父对象，确保卡牌UI在最顶层显示
        Card card = go.GetComponent<Card>();
        card.isMoving = true;
        card.hasUse = false;
        go.transform.DOMove(transform.position, 0.8f).OnComplete(
            () =>
            {
                go.transform.SetParent(transform, false);
                //移动到其父对象的子物体列表的最前面
                go.transform.SetAsFirstSibling();
                card.isMoving = false;
            }
        );
    }
}


