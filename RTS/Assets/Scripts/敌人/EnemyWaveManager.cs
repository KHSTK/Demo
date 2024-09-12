using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }
    public event EventHandler OnWaveNumberChanged;// 波次编号变化事件

    private enum State
    {
        WaitingToSpawnNextWave,  // 等待生成下一波敌人
        SpawningWave             // 正在生成敌人波次
    }

    [SerializeField] private List<Transform> spawnPositionTransformList; // 敌人生成位置列表
    [SerializeField] private Transform nextWaveSpawnPositionTransform;//生成位置指示
    private State state;                        // 当前状态
    private int waveNumber;                      // 波次数
    private float nextWaveSpawnTimer;            // 下一波生成计时器
    private float nextEnemySpawnTimer;           // 下一个敌人生成计时器
    private int remainingEnemySpawnAmount;       // 剩余敌人生成数量
    private Vector3 spawnPosition;               // 当前生成位置

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;     // 初始状态为等待生成下一波敌人
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;  // 随机选择一个生成位置
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;                  // 下一波生成计时器初始值为3秒 开始等待3秒
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;  // 减少下一波生成计时器
                if (nextWaveSpawnTimer <= 0f)          // 当计时器小于等于0时开始生成新的波次
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;   // 减少下一个敌人生成计时器
                    if (nextEnemySpawnTimer <= 0f)           // 当计时器小于等于0时生成敌人，并更新剩余敌人生成数量
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 2f);
                        Enemy.Create(spawnPosition + Utilsclass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                    }
                }
                if (remainingEnemySpawnAmount <= 0)        // 当剩余敌人生成数量小于等于0时，切换回等待生成下一波敌人的状态
                {
                    state = State.WaitingToSpawnNextWave;
                    spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;  // 随机选择一个生成位置
                    nextWaveSpawnPositionTransform.position = spawnPosition;
                }
                break;
        }
    }

    private void SpawnWave()
    {
        nextWaveSpawnTimer = 10f;           // 下一波生成计时器为10秒
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;   // 剩余敌人生成数量根据波次数计算
        state = State.SpawningWave;         // 设置当前状态为正在生成敌人波次
        waveNumber++;                       // 波次数递增
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);// 触发波次编号变化事件
    }

    public int GetWaveNumber()
    {
        // 返回当前波次数
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        // 返回下一波敌人生成计时器的剩余时间
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        // 返回当前敌人生成位置
        return spawnPosition;
    }
}
