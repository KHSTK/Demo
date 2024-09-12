using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    TextMeshProUGUI waveSurvivedText;
    public static GameOverUI Instance { get; private set; } // ��̬ʵ��

    private void Awake()
    {
        Instance = this; // ʵ������̬ʵ��
        waveSurvivedText = transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>());
        // ��ȡ���԰�ť����ӵ���¼�������
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        Hide(); // ����UI����
    }

    public void Show()
    {
        gameObject.SetActive(true); // ��ʾUI����

        // ��ȡWaveSurvivedText�������ı�����
        waveSurvivedText.SetText("������� " + EnemyWaveManager.Instance.GetWaveNumber() + " ��!");
    }

    public void Hide()
    {
        gameObject.SetActive(false); // ����UI����
    }
}
