using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    [SerializeField]
    [Range(1, 7)]
    private int moveRange = 1;
    [SerializeField] private float moveSpeed;
    public bool hasMove;
    private void OnMouseDown()
    {
        GameManage.instance.selectedUnit = this;
        ShowWalkableTiles();
    }
    private void ShowWalkableTiles()
    {
        //if (hasMove)
        //{
        //    return;
        //}
        for (int i = 0; i < GameManage.instance.tiles.Length; i++)
        {
            float distX = Mathf.Abs(transform.position.x - GameManage.instance.tiles[i].transform.position.x);
            float distY = Mathf.Abs(transform.position.y - GameManage.instance.tiles[i].transform.position.y);

                if (distX + distY <= moveRange)
                {
                    if (GameManage.instance.tiles[i].canWalk)
                        GameManage.instance.tiles[i].HighlightTile();
                }
        }
    }

    public void Move(Transform trans)
    {
        StartCoroutine(MoveCo(trans));
    }
    IEnumerator MoveCo(Transform trans)
    {
        while (transform.position.x != trans.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(trans.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != trans.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, trans.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
        ResetTiles();
    }
    public void ResetTiles()
    {
        for (int i = 0; i < GameManage.instance.tiles.Length; i++)
        {
            GameManage.instance.tiles[i].ResetTile();
        }
    }
}


