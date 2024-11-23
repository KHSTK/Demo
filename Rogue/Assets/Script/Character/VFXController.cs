using UnityEngine;

public class VFXController : MonoBehaviour
{
    public GameObject buff;
    public GameObject debuff;
    private float timeCounter;
    private void Update()
    {
        if (buff.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 0.65f)
            {
                buff.SetActive(false);
                timeCounter = 0f;
            }
        }
        if (debuff.activeInHierarchy)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 0.65f)
            {
                debuff.SetActive(false);
                timeCounter = 0f;
            }
        }
    }
}
