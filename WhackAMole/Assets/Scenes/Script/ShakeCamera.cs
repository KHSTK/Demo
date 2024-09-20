using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float Shaketime;
    public float Shakerange;
    bool starShake=false;
    public static ShakeCamera _instanc;
    private void Awake()
    {
        _instanc = this;
    }
    void Update()
    {
        if (starShake)
        {
            StartCoroutine(Shake(Shaketime, Shakerange));
            starShake = false;
        }
    }

    public IEnumerator Shake(float Shaketime, float Shakerange)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < Shaketime)
        {
            float x = Random.Range(-1f, 1f) * Shakerange;
            float y = Random.Range(-1f, 1f) * Shakerange;
            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;

        }
        transform.position = orignalPosition;
    }
    public void returnShake()
    {
        starShake = true;
    }
}
