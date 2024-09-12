using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    TextMeshProUGUI waveSurvivedText;
    public static GameOverUI Instance { get; private set; } // 静态实例

    private void Awake()
    {
        Instance = this; // 实例化静态实例
        waveSurvivedText = transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>());
        // 获取重试按钮并添加点击事件监听器
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        Hide(); // 隐藏UI界面
    }

    public void Show()
    {
        gameObject.SetActive(true); // 显示UI界面

        // 获取WaveSurvivedText并设置文本内容
        waveSurvivedText.SetText("您坚持了 " + EnemyWaveManager.Instance.GetWaveNumber() + " 波!");
    }

    public void Hide()
    {
        gameObject.SetActive(false); // 隐藏UI界面
    }
}
