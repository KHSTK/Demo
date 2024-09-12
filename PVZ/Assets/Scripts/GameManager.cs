using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject sunPrefab;
    public int sunSum;//��������
    public int curLevelId = 1; //��ǰ�ؿ�
    public int curProgressId = 1;//��ǰ����
    public List<Dictionary<string, string>> listData;//��ǰ�ؿ�����
    public bool isStart;//�Ƿ�ʼ��Ϸ
    public ProgressPanel progressPanel;
    public void Start()
    {
        isStart = false;
    }
    //��ʼ��Ϸ
    public void StartGame()
    {
        //AudioManager.Instance.PlayMusic("bgm1");
        isStart = true;
        //�������λ�õ�������
        InvokeRepeating("CreateSunDown", 10, 10);
        // ����������
        if (progressPanel != null)
        {
            progressPanel.gameObject.SetActive(true);
        }
        //���ɽ�ʬ
        GenerateZombies.Instance.TableCreateZombie();
        
    } 
    private void Awake()
    {
        Instance = this;
        //��ʼ�����ñ�
        GameConfigManager.Instance.Init();
        //��ȡ��ǰ�ؿ�����
        listData = GameConfigManager.Instance.GetLevelList(curLevelId.ToString());
    }

    public void SetSunSum(int num)
    {
        sunSum += num;
        UIManager.Instance.Init();
    }
    public void CreateSunDown()
    {
        //��ȡ���½ǡ����Ͻǵ���������
        Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);
        float x = Random.Range(leftBottom.x+50 , rightTop.x-400);
        Vector3 bornPos = new Vector3(x, rightTop.y, 0);
        GameObject sun = Instantiate(sunPrefab, bornPos, Quaternion.identity);
        float y = Random.Range(leftBottom.y + 90, rightTop.y - 50);
        sun.GetComponent<Sun>().SetTargetPos(new Vector3(x, y, 0));
    }

}

