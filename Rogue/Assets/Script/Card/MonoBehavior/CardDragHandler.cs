using UnityEngine;
using UnityEngine.EventSystems;


public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;
    private GameObject currentArrow;
    private CardDeck cardDeck; //引用卡组
    private Card currentCard;
    private bool canMove;
    private bool canExecute;
    private Vector3 worldPos;
    private CharacterBase targetCharacter;

    private void Awake()
    {
        currentCard = GetComponent<Card>();
        cardDeck = GameObject.FindGameObjectWithTag("CardDeck").GetComponent<CardDeck>();
    }
    private void OnDisable()
    {
        currentCard.transform.Find("Entry/Use").gameObject.SetActive(false);
        currentCard.transform.Find("Entry/Dis").gameObject.SetActive(false);
        canMove = false;
        canExecute = false;
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!currentCard.isAvailable && !cardDeck.needDiscard) return;
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
            // currentArrow = Instantiate(arrowPrefab, currentCard.transform.position, Quaternion.identity);
            // break;
            case CardType.Defense:
            case CardType.Buff:
                canMove = true;
                break;
        }
    }

    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        if (canMove)
        {
            currentCard.isAnimating = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;
            if (cardDeck.needDiscard && worldPos.y > 0.5f)
            {
                currentCard.transform.Find("Entry/Dis").gameObject.SetActive(true);
            }
            else if (worldPos.y > 0.5f)
            {
                canExecute = true;
                targetCharacter = GameObject.FindWithTag("Enemy").GetComponent<CharacterBase>();
                currentCard.transform.Find("Entry/Use").gameObject.SetActive(true);
            }
            else
            {

                canExecute = false;
                targetCharacter = null;
                currentCard.transform.Find("Entry/Use").gameObject.SetActive(false);
            }
        }
        // else
        // {
        //     if (eventData.pointerEnter == null) return;
        //     if (eventData.pointerEnter.CompareTag("Enemy"))
        //     {
        //         canExecute = true;
        //         targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
        //         return;
        //     }
        //     canExecute = false;
        //     targetCharacter = null;
        // }
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        // if (currentArrow != null)
        // {
        //     Destroy(currentArrow);
        // }
        if (cardDeck.needDiscard)
        {
            cardDeck.DiscardCard(currentCard);
            cardDeck.OnPlayerTurnEnd();
        }
        if (canExecute)
        {
            Debug.Log("执行");
            Debug.Log(targetCharacter);
            currentCard.ExecuteCardEffect(currentCard.player, targetCharacter);
        }
        else
        {
            currentCard.RestCardTransform();
        }

    }
}
