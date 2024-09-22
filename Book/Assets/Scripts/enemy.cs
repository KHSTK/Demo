using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //[SerializeField] private List<GameObject> enemyPoint = new List<GameObject>();

    private void Start()
    {
        path pf = FindObjectOfType<path>();
        var minPath = pf.GetPoints();
        StartCoroutine(FindWayPoint(minPath));
    }
    IEnumerator FindWayPoint(List<WayPoint> pathwayPoints)
    {
        foreach(WayPoint wayPoint in pathwayPoints)
        {
            transform.position = wayPoint.transform.position + new Vector3(0, 0,0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
