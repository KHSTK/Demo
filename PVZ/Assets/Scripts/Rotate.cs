using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}

