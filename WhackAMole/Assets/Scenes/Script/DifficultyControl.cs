using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyControl : MonoBehaviour
{
    public float Interval;
    public float LiveTime;
    public float GameTime;
    public bool setNexts;
    public int passScore;
    public static bool Next;
    public static bool pass;
    public static int Score; 

    // Start is called before the first frame update
    void Start()
    {
        Mole.interval = Interval;
        Hold.interval = LiveTime;
        GameActive.Timenum = GameTime;
        Next = setNexts;
        pass = false;
        Score = passScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
