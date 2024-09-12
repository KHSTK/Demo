using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WallNut : Plants
{
    private Character character;
    private Animator animator;
    float crackedPoint1,crackedPoint2;

    private void Awake()
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();

        if (character == null)
        {
            Debug.LogError("Character component not found on " + gameObject.name);
        }

        crackedPoint1 = character.maxHealth * 2 / 3;
        crackedPoint2 = character.maxHealth / 3;
    }

    private void Update()
    {
        if (character.currentHealth< crackedPoint1)
        {
            animator.SetBool("crackedPoint1", true);
        }
        if (character.currentHealth< crackedPoint2)
        {
            animator.SetBool("crackedPoint2", true);
        }
    }
    public void OnDie()
    {
        Destroy(gameObject);
    }
}
