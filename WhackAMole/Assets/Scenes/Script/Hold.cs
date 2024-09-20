using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Transform Mole;
    float time = 0f;
    public static float interval = 3f;//Ë¢ÐÂÊ±¼ä
    // Start is called before the first frame update
    void Update()
    {
        if (MoleActive())
        {
            time += Time.deltaTime;
        }
        if (time >= interval)
        {
            Mole.gameObject.SetActive(false);
            time = 0f;
        }
    }
    public bool  MoleActive()
    {
        return Mole.gameObject.activeSelf;
    }
    
}
