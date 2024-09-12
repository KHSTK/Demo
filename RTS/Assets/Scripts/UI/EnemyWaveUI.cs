using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager; // 敌人波次管理器

    private TextMeshProUGUI waveNumberText; // 显示波次编号的文本
    private TextMeshProUGUI waveMessageText; // 显示波次消息的文本
    private RectTransform enemyWaveSpawnPositionIndicator;  // 敌人指示器
    private Camera mainCamera;  // 主摄像机

    private void Awake()
    {
        // 获取波次编号文本和波次消息文本的引用
        waveNumberText = transform.Find("波数").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("倒计时").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 订阅敌人波次管理器的波次编号变化事件
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        enemyWaveSpawnPositionIndicator = transform.Find("敌人指示器").GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        // 当波次编号发生变化时，更新波次编号文本显示
        Debug.Log("波数 " + enemyWaveManager.GetWaveNumber());

        SetWaveNumberText("波数 " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();

        if (nextWaveSpawnTimer < 0f)
        {
            // 如果下一波次生成的计时器小于0，则清空波次消息文本
            SetMessageText("");
        }
        else
        {
            // 显示下一波次生成的计时器
            SetMessageText("下波倒计时 " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
        // 计算指向下一个生成位置的方向向量
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        // 将指示器的位置设置为方向向量的长度乘以一个常数
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 500f;
        // 根据方向向量计算角度，并将指示器旋转到该角度
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, Utilsclass.GetAngleFromVector(dirToNextSpawnPosition));
        // 计算当前位置与下一个生成位置之间的距离
        float distanceToNextspawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        // 根据距离判断是否显示指示器
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextspawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void SetMessageText(string message)
    {
        // 更新波次消息文本显示
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        // 更新波次编号文本显示
        waveNumberText.SetText(text);
    }
}
