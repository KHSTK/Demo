using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/EnemyActionData")]
public class EnemyActionDataSO : ScriptableObject
{
    public List<EnemyAction> actionList;
}
[System.Serializable]
public struct EnemyAction
{
    public Sprite initentIcon;
    public Effect effect;
}