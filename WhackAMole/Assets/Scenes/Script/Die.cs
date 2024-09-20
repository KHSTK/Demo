using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("°´ÏÂ");
        gameObject.SetActive(false);
        GameActive._instanc.AddScore();
        ShakeCamera._instanc.returnShake();
        Music._instanc.hitSuccess();
    }
    
}

