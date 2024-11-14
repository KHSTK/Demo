using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool pool;
    public List<CardDataSO> cardDataList;
    private void Awake()
    {
        InitCardList();
    }
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
}
