using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostHead : MonoBehaviour
{
    public float existenceTime = 1.5f;//����ʱ��

    private void Start()
    {
        Destroy(gameObject, existenceTime);
    }
}

