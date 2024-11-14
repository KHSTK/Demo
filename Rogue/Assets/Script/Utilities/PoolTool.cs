using UnityEngine;
using UnityEngine.Pool;
public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;
    public ObjectPool<GameObject> pool;
    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(objPrefab, transform),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
        PreFillPool(7);
    }
    //预先生成对象
    private void PreFillPool(int count)
    {
        var preFillArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            preFillArray[i] = pool.Get();
        }
        foreach (var obj in preFillArray)
        {
            pool.Release(obj);
        }

    }
    //获取对象
    public GameObject GetGameObjectFromPool()
    {
        return pool.Get();
    }
    //释放对象
    public void ReleaseGameObjectToPool(GameObject obj)
    {
        pool.Release(obj);
    }
}
