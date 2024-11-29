using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        animator.Play("sleep");
        animator.SetBool("isSleep", true);
    }
    private void Update()
    {
        if (player.defense.currentValue > 0)
        {
            animator.SetBool("isParry", true);
        }
        else
        {
            animator.SetBool("isParry", false);
        }
    }
    public void PlayerTurnStar()
    {
        animator.SetBool("isSleep", false);
    }
    public void PlayerTurnEnd()
    {
        if (player.defense.currentValue < 1)
        {
            animator.SetBool("isSleep", true);
        }
    }
    public void OnCardEffect(object obj)
    {
        Card card = obj as Card;
        switch (card.cardData.cardType)
        {
            case CardType.Attack:
                animator.SetTrigger("attack");
                break;
            case CardType.Defense:
            case CardType.Buff:
                animator.SetTrigger("skill");
                break;
        }
    }
    public void SetPlayerSleep()
    {
        Debug.Log("SetPlayerSleep");
        animator.Play("death");
    }
}