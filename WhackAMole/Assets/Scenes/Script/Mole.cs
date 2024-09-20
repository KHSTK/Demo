using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    private Transform randomHold;
    private Transform[] Holds;
    // Start is called before the first frame update
    void Start()
    {
        GetHold();
        ShowMole(randomHold);
    }
    float time = 0f;
    public static float interval = 1f;//间隔时间
    public void Update()
    {
        if (GameActive._instanc.GetTime()&&GameActive._instanc.StarGame()) { 
        GetHold();
        time += Time.deltaTime;
            if (time >= interval&& !randomHold.GetComponent<Hold>().MoleActive())
            {
            ShowMole(randomHold);
            time = 0f;
            }
        }
        else
        {
            HoldDown();
        }
    }
    private void  GetHold()
    {

        Holds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Holds[i] = transform.GetChild(i);
        }
        int randomIndex = Random.Range(0, Holds.Length);
        randomHold = Holds[randomIndex];
        Debug.Log("寻找一次未标记");
    }
    private void HoldDown()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Holds[i] = transform.GetChild(i);
            Transform Hold = Holds[i];
            Hold.Find("Mole").gameObject.SetActive(false);
        }
    }
    private void ShowMole(Transform randomHold)
    {
        Transform Mole = randomHold.Find("Mole");
        Mole.gameObject.SetActive(true);
    }
}
