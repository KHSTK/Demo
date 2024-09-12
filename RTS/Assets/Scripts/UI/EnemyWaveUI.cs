using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager; // ���˲��ι�����

    private TextMeshProUGUI waveNumberText; // ��ʾ���α�ŵ��ı�
    private TextMeshProUGUI waveMessageText; // ��ʾ������Ϣ���ı�
    private RectTransform enemyWaveSpawnPositionIndicator;  // ����ָʾ��
    private Camera mainCamera;  // �������

    private void Awake()
    {
        // ��ȡ���α���ı��Ͳ�����Ϣ�ı�������
        waveNumberText = transform.Find("����").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("����ʱ").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // ���ĵ��˲��ι������Ĳ��α�ű仯�¼�
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        enemyWaveSpawnPositionIndicator = transform.Find("����ָʾ��").GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        // �����α�ŷ����仯ʱ�����²��α���ı���ʾ
        Debug.Log("���� " + enemyWaveManager.GetWaveNumber());

        SetWaveNumberText("���� " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();

        if (nextWaveSpawnTimer < 0f)
        {
            // �����һ�������ɵļ�ʱ��С��0������ղ�����Ϣ�ı�
            SetMessageText("");
        }
        else
        {
            // ��ʾ��һ�������ɵļ�ʱ��
            SetMessageText("�²�����ʱ " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
        // ����ָ����һ������λ�õķ�������
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        // ��ָʾ����λ������Ϊ���������ĳ��ȳ���һ������
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 500f;
        // ���ݷ�����������Ƕȣ�����ָʾ����ת���ýǶ�
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, Utilsclass.GetAngleFromVector(dirToNextSpawnPosition));
        // ���㵱ǰλ������һ������λ��֮��ľ���
        float distanceToNextspawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        // ���ݾ����ж��Ƿ���ʾָʾ��
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextspawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void SetMessageText(string message)
    {
        // ���²�����Ϣ�ı���ʾ
        waveMessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        // ���²��α���ı���ʾ
        waveNumberText.SetText(text);
    }
}
