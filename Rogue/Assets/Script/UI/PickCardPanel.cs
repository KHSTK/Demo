using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickCardPanel : MonoBehaviour
{
    public CardManager cardManager;
    private VisualElement rootElement;
    public VisualTreeAsset cardTemplate;
    private VisualElement cardContainer;
    private Button pickOverButton;
    private CardDataSO currentCardData;
    [SerializeField]
    private List<CardDataSO> addCardDataList = new List<CardDataSO>();
    private List<Button> cardButtonList = new List<Button>();
    [Header("广播")]
    public ObjectEventSO pickOverEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        cardContainer = rootElement.Q<VisualElement>("Container");
        pickOverButton = rootElement.Q<Button>("PickOverButton");
        pickOverButton.clicked += OnPickOverButtonClicked;
        for (int i = 0; i < 3; i++)
        {
            var card = cardTemplate.Instantiate();
            var cardData = cardManager.GetRandomCardData();
            InitCard(card, cardData);
            var cardButton = card.Q<Button>("Card");
            cardButtonList.Add(cardButton);
            cardButton.clicked += () => OnCardClicked(cardButton, cardData);
            card.style.height = 330;
            card.style.width = 240;
            cardContainer.Add(card);
        }
    }

    private void OnPickOverButtonClicked()
    {
        cardManager.AddNewCardToLibrary(addCardDataList);
        pickOverEvent?.RaiseEvent(null, this);
    }

    private void OnCardClicked(Button cardButton, CardDataSO cardData)
    {
        currentCardData = cardData;
        Debug.Log("点击卡的内容：" + currentCardData.cardName);
        addCardDataList.Add(currentCardData);
        cardButton.SetEnabled(false);
    }

    private void InitCard(VisualElement card, CardDataSO cardData)
    {
        card.dataSource = cardData;
        var cardSpricte = card.Q<VisualElement>("CardSprite");
        var cardName = card.Q<Label>("NameText");
        var cardType = card.Q<Label>("TypeText");
        var cardDescription = card.Q<Label>("DescriptionText");
        var cardCost = card.Q<Label>("CostText");

        cardSpricte.style.backgroundImage = new StyleBackground(cardData.cardSprite);
        cardName.text = cardData.cardName;
        cardDescription.text = cardData.description;
        cardCost.text = cardData.cardCost.ToString();
        cardType.text = cardData.cardType switch
        {
            CardType.Attack => "攻击",
            CardType.Defense => "防御",
            CardType.Buff => "技能",
            _ => throw new System.NotImplementedException(),
        };

    }
}
