using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/建筑类型列表")]
public class BuildingTypeList : ScriptableObject
{
    public List<BuildingType> buildingTypeList; // 建筑类型列表
}

