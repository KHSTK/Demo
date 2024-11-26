using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool pool;
    public List<CardDataSO> cardDataList;//游戏中所有卡牌
    [Header("卡牌库")]
    public CardLibrarySO newGameLibrary;//新游戏卡牌库
    public CardLibrarySO currentLibrary;//当前玩家卡牌库
    private int preIndex;
    private void Awake()
    {
        InitCardList();
        foreach (var item in newGameLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
    }
    private void OnDisable()
    {
        currentLibrary.cardLibraryList.Clear();
    }

    /// <summary>
    /// 初始化卡牌列表
    /// </summary>
    private void InitCardList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSO>(handle.Result);
        }
        else
        {
            Debug.LogError("Failed to load card data");
        }
    }

    /// <summary>
    /// 抽卡时调用该函数获取卡牌GameObject
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardObject()
    {

        var currentCardObject = pool.GetGameObjectFromPool();
        currentCardObject.transform.localScale = Vector3.zero;
        return currentCardObject;
    }
    /// <summary>
    /// 回收卡牌时调用该函数
    /// </summary>
    /// <param name="cardObject"></param>
    public void RecycleCardObject(GameObject cardObject)
    {
        pool.ReleaseGameObjectToPool(cardObject);
    }
    public CardDataSO GetRandomCardData()
    {
        var randomIndex = 0;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardDataList.Count);
        } while (randomIndex == preIndex);//防止抽到相同的卡牌
        preIndex = randomIndex;
        return cardDataList[preIndex];
    }
    /// <summary>
    /// 解锁新卡牌
    /// </summary>
    /// <param name="cardData"></param>
    public void AddNewCardToLibrary(List<CardDataSO> addCardDataList)
    {
        foreach (var card in addCardDataList)
        {
            var newCard = new CardLibraryEntry
            {
                cardData = card,
                amount = 1
            };
            // 查找是否已存在该卡牌
            bool cardExists = false;
            for (int i = 0; i < currentLibrary.cardLibraryList.Count; i++)
            {
                if (currentLibrary.cardLibraryList[i].cardData == card) // 检查引用是否相同
                {
                    // 将当前元素赋值给一个变量
                    CardLibraryEntry entry = currentLibrary.cardLibraryList[i];
                    entry.amount++; // 修改变量
                    currentLibrary.cardLibraryList[i] = entry; // 将修改后的变量重新赋值给列表
                    Debug.Log("增加卡牌数");
                    Debug.Log(currentLibrary.cardLibraryList[i].cardData.cardName + "数量：" + currentLibrary.cardLibraryList[i].amount);
                    cardExists = true;
                    break; // 找到后退出循环
                }
            }
            if (!cardExists)
            {
                Debug.Log("添加新卡");
                currentLibrary.cardLibraryList.Add(newCard);
            }
        }
    }
}
