using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Card/CardLibrary")]
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList;
}
[System.Serializable]
public struct CardLibraryEntry
{
    public CardDataSO cardData;
    public int amount;
}