using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour, IInteractable
{
    public bool isOpen;//控制植物是否运行

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
