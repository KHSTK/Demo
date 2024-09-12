using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }
    public event EventHandler OnWaveNumberChanged;// ���α�ű仯�¼�

    private enum State
    {
        WaitingToSpawnNextWave,  // �ȴ�������һ������
        SpawningWave             // �������ɵ��˲���
    }

    [SerializeField] private List<Transform> spawnPositionTransformList; // ��������λ���б�
    [SerializeField] private Transform nextWaveSpawnPositionTransform;//����λ��ָʾ
    private State state;                        // ��ǰ״̬
    private int waveNumber;                      // ������
    private float nextWaveSpawnTimer;            // ��һ�����ɼ�ʱ��
    private float nextEnemySpawnTimer;           // ��һ���������ɼ�ʱ��
    private int remainingEnemySpawnAmount;       // ʣ�������������
    private Vector3 spawnPosition;               // ��ǰ����λ��

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;     // ��ʼ״̬Ϊ�ȴ�������һ������
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;  // ���ѡ��һ������λ��
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;                  // ��һ�����ɼ�ʱ����ʼֵΪ3�� ��ʼ�ȴ�3��
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;  // ������һ�����ɼ�ʱ��
                if (nextWaveSpawnTimer <= 0f)          // ����ʱ��С�ڵ���0ʱ��ʼ�����µĲ���
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemySpawnTimer -= Time.deltaTime;   // ������һ���������ɼ�ʱ��
                    if (nextEnemySpawnTimer <= 0f)           // ����ʱ��С�ڵ���0ʱ���ɵ��ˣ�������ʣ�������������
                    {
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 2f);
                        Enemy.Create(spawnPosition + Utilsclass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;
                    }
                }
                if (remainingEnemySpawnAmount <= 0)        // ��ʣ�������������С�ڵ���0ʱ���л��صȴ�������һ�����˵�״̬
                {
                    state = State.WaitingToSpawnNextWave;
                    spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;  // ���ѡ��һ������λ��
                    nextWaveSpawnPositionTransform.position = spawnPosition;
                }
                break;
        }
    }

    private void SpawnWave()
    {
        nextWaveSpawnTimer = 10f;           // ��һ�����ɼ�ʱ��Ϊ10��
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;   // ʣ����������������ݲ���������
        state = State.SpawningWave;         // ���õ�ǰ״̬Ϊ�������ɵ��˲���
        waveNumber++;                       // ����������
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);// �������α�ű仯�¼�
    }

    public int GetWaveNumber()
    {
        // ���ص�ǰ������
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer()
    {
        // ������һ���������ɼ�ʱ����ʣ��ʱ��
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        // ���ص�ǰ��������λ��
        return spawnPosition;
    }
}
