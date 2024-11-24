using UnityEngine;

public class Player : CharacterBase
{
    public IntVariable playerEnergy;
    public int CurrentEnergy { get => playerEnergy.currentValue; set => playerEnergy.SetValue(value); }
    public int maxEnergy;
    public int MaxEnergy { get => playerEnergy.maxValue; }
    protected override void Start()
    {
        base.Start();
        playerEnergy.maxValue = maxEnergy;
        CurrentEnergy = MaxEnergy;
    }
    /// <summary>
    /// 每回合逻辑，监听事件
    /// </summary>
    public void NewTurn()
    {
        CurrentEnergy = maxEnergy;
    }
    public void UseEnergy(int cost)
    {
        if (CurrentEnergy >= cost)
        {
            CurrentEnergy += cost;
        }
        else
        {
            Debug.Log("能量不足");
        }
    }
}