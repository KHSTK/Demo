using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool isExplored;
    public WayPoint exploredFrom;
    private void Start()
    {
        //Debug.Log(gameObject.name + ":" + GetPosition());
    }
    public Vector2Int GetPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
            );
    }
}
