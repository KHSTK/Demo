using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    [SerializeField] private GameObject starPoint, endPoint;

    public Dictionary<Vector2Int, WayPoint> wayPointDict = new Dictionary<Vector2Int, WayPoint>();
    private Vector2Int[] directions =
    {
        Vector2Int.up,Vector2Int.right,Vector2Int.down,Vector2Int.left
    };
    Queue<WayPoint> queue = new Queue<WayPoint>();
    [SerializeField] private bool isRunning = true;

    private WayPoint searchCenter;
    public List<WayPoint> minPath = new List<WayPoint>();
    //private void Start()
    //{
    //    LoadAllWayPoints();
    //    //ExploreAround();
    //    BFS();
    //    CreatPath();
    //}
    public List<WayPoint> GetPoints()
    {
        LoadAllWayPoints();
        //ExploreAround();
        BFS();
    CreatPath();
        return minPath;
    }
    private void ExploreAround()
    {
        if (isRunning == false)
        {
            return;
        }
        foreach(Vector2Int dirction in directions)
        {
            //Debug.Log("Exploring:"+starPoint.GetComponent<WayPoint>().GetPosition()+dirction);
            var explareArounds = searchCenter.GetPosition() + dirction;
            try
            {
                var neighbour = wayPointDict[explareArounds];
                if (neighbour.isExplored||queue.Contains(neighbour))
                {

                }
                else
                {
                    queue.Enqueue(neighbour);
                    Debug.Log("Exploring:" + explareArounds);
                    neighbour.exploredFrom = searchCenter;
                }
               
            }
            catch
            {
            }
        }
    }
    private void LoadAllWayPoints()
    {
        var wayPoints = FindObjectsOfType<WayPoint>();
        foreach(WayPoint wayPoint in wayPoints)
        {
            var tempWayPoint = wayPoint.GetPosition();
            if (wayPointDict.ContainsKey(tempWayPoint))
            {
                Debug.Log("Skip overlap vlock" + wayPoint);
            }
            else
            {
                wayPointDict.Add(tempWayPoint, wayPoint);
            }
        }
    }
    private void BFS()
    {
        queue.Enqueue(starPoint.GetComponent<WayPoint>());
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            Debug.Log("Search From:" + searchCenter.GetPosition());
            StopIfSearchEnd();
            ExploreAround();
            searchCenter.isExplored = true;
        }
    }
    private void StopIfSearchEnd()
    {
        if (searchCenter == endPoint.GetComponent<WayPoint>())
        {
            isRunning = false;
            Debug.Log("Stop");
        }
    }
    private void CreatPath()
    {
        minPath.Add(endPoint.GetComponent<WayPoint>());//终点信息
        WayPoint prePoint = endPoint.GetComponent<WayPoint>().exploredFrom;//终点前一个点
        while (prePoint != starPoint.GetComponent<WayPoint>())//如果前一个点不是起点
        {
            minPath.Add(prePoint);//存储在list
            prePoint = prePoint.exploredFrom;//前一个点的前一个点
        }
        minPath.Add(starPoint.GetComponent<WayPoint>());
        minPath.Reverse();
    }
}
