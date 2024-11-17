using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;

public class CardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Card currtentCard;
    private bool canMove;
    private bool canExecute;
    private Vector3 worldPos;
    private void Awake()
    {
        currtentCard = GetComponent<Card>();
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (currtentCard.cardData.cardType)
        {
            case CardType.Attack:
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
            currtentCard.isAnimating = true;
            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currtentCard.transform.position = worldPos;
        }
    }

    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        canExecute = worldPos.y > 0;
        if (canExecute)
        {

        }
        else
        {
            currtentCard.RestCardTransform();
        }
    }
}
