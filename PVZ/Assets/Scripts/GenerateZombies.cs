using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateZombies : MonoBehaviour
{
    public static GenerateZombies Instance { get; private set; }
    public List<GameObject> curProgressZombie;//���浱ǰ���ȵĵ���
    int zOrderIndex = 0;//����


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        curProgressZombie = new List<GameObject>();
    }

    //���ɽ�ʬ
    public void TableCreateZombie()
    {
        //�ж��Ƿ������һ�����ˣ��������е�ǰ����û�п��Դ����ĵ��ˣ�����Ϸʤ��
        bool canCreate = false;

        //��ȡ��ǰ�ؿ�����
        GameManager.Instance.listData.ForEach(data =>
        {
            //���ڵ�ǰ���ȵĽ�ʬ
            if (data["progressId"] == GameManager.Instance.curProgressId.ToString())
            {
                //�ӳ�һ��ʱ�䴴����ʬ
                StartCoroutine(ITableCreateZombie(data));
                //����ǰ�����е���
                canCreate = true;
            }
        });

        if (!canCreate)
        {
            StopAllCoroutines();//ֹͣ���е�Я��
        //TODO: ��Ϸʤ������
            Debug.Log("��Ϸʤ��");
        }
    }

    IEnumerator ITableCreateZombie(Dictionary<string, string> levelItem)
    {
        yield return new WaitForSeconds(float.Parse(levelItem["createTime"]));

        //����Ԥ�Ƽ�����Resources�ļ����м��أ�����Zombie1
        GameObject zombiePrefab = Resources.Load("Prefabs/Enemy/Zombie" + levelItem["zombieType"]) as GameObject;

        //���ɽ�ʬʵ��
        GameObject zombie = Instantiate(zombiePrefab);

        //������������λ�ã��ҵ�������
        Transform zombieLine = transform.GetChild(int.Parse(levelItem["bornPos"]));
        zombie.transform.parent = zombieLine;
        zombie.transform.localPosition = Vector3.zero;
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex;
        zOrderIndex++;

        curProgressZombie.Add(zombie);
    }

    //�������
    public void ZombieDied(GameObject gameObject)
    {
        if (curProgressZombie.Contains(gameObject))
        {
            curProgressZombie.Remove(gameObject);
            //���½���UI
            UIManager.Instance.UpdateProgressPanel();
        }
        //��ǰ���ȵĽ�ʬȫ�������ˣ�������һ������
        if (curProgressZombie.Count == 0)
        {
            GameManager.Instance.curProgressId += 1;
            TableCreateZombie();
        }
    }
}


