using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour, IInteractable
{
    public bool isOpen;//����ֲ���Ƿ�����

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
