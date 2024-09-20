using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieMole : MonoBehaviour
{
    public static DieMole _instanc;
    bool die = false;
    private void Awake()
    {
        _instanc = this;
    }
    void Update()
    {
        if (die)
        {
            StartCoroutine(Die());
            die = false;
        }
    }

    public IEnumerator Die()
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            gameObject.SetActive(true);
            yield return 0;

        }
            gameObject.SetActive(true);
    }
    public void ReturnDie()
    {
        die = true;
    }
}
