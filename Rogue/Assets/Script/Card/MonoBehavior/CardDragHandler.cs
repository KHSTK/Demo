using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject arrowPrefab;
    private GameObject currentArrow;

    private Card currentCard;
    private bool canMove;
    private bool canExecute;
    private Vector3 worldPos;
    private CharacterBase targetCharacter;

    private void Awake()
    {
        currentCard = GetComponent<Card>();
    }
    private void OnDisable()
    {
        currentCard.transform.Find("Entry/Square").gameObject.SetActive(false);
        canMove = false;
        canExecute = false;
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currentCard.cardData.cardType)
        {
            case CardType.Attack:
                currentArrow = Instantiate(arrowPrefab, currentCard.transform.position, Quaternion.identity);
                break;
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
            if (worldPos.y > 0.5f)
            {
                canExecute = true;
                currentCard.transform.Find("Entry/Square").gameObject.SetActive(true);
            }
            else
            {
                canExecute = false;
                currentCard.transform.Find("Entry/Square").gameObject.SetActive(false);
            }
        }
        else
        {
            if (eventData.pointerEnter == null) return;
            if (eventData.pointerEnter.CompareTag("Enemy"))
            {
                canExecute = true;
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                return;
            }
            canExecute = false;
            targetCharacter = null;
        }
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }
        if (canExecute)
        {
            Debug.Log("执行");
            currentCard.ExecuteCardEffect(currentCard.player, targetCharacter);
        }
        else
        {
            currentCard.RestCardTransform();
        }

    }
}
