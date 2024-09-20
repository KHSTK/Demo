using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music _instanc;
    public AudioClip Hit;
    public AudioClip HitSuccess;
    public AudioSource audioSource;
    private void Awake()
    {
        _instanc = this;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(Hit);
            Debug.Log("点击声");
        }
    }
    public void hitSuccess()
    {
        audioSource.PlayOneShot(HitSuccess);
        Debug.Log("点击成功声");
    }
}
