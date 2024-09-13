using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;         // ��Ƶ����������
    public AudioClip clip;      // ��Ƶ����
    [Range(0f, 1f)]
    public float volume = 0.7f; // ������С
}

