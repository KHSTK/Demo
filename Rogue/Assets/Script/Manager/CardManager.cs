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
        
        var currentCardObject= pool.GetGameObjectFromPool();
        currentCardObject.transform.localScale= Vector3.zero;
        return currentCardObject;
    }
    //外部回收卡牌
    public void RecycleCardObject(GameObject cardObject)
    {
        pool.ReleaseGameObjectToPool(cardObject);
    }
}
