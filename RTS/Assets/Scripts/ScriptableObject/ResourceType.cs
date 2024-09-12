using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/资源类型")]
public class ResourceType : ScriptableObject
{
    public Sprite sprite; // 资源的图标
    public string nameString; // 资源类型的名称
}

