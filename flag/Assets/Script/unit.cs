using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    [SerializeField] [Range(1,7)]
    private int moveRange = 3;
    [SerializeField] private float moveSpeed=1;
    public bool hasMove;
    private void OnMouseDown()
    {
        GameManager.instance.selectedUnit = this;
        ShowWalkableTiles();
    }
    private void ShowWalkableTiles()
    {
        if (hasMove)
        {
            return;
        }
        for (int i = 0; i < GameManager.instance.titles.Length; i++)
        {
            float disX = Mathf.Abs(transform.position.x - GameManager.instance.titles[i].transform.position.x);
            float disY = Mathf.Abs(transform.position.y - GameManager.instance.titles[i].transform.position.y);
            if (disX + disY <= moveRange)
            {
                if (GameManager.instance.titles[i].canWalk)
                {
                    GameManager.instance.titles[i].HighlightTile();
                }
            }
        }
    }
    public void Move(Transform _trans)
    {
        StartCoroutine(MoveCo(_trans));
    }
    IEnumerator MoveCo(Transform _trans)
    {
        while (transform.position.x != _trans.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_trans.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != _trans.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _trans.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
        hasMove = true;
        ResetTile();
    }
    private void ResetTile()
    {
        for (int i = 0; i < GameManager.instance.titles.Length; i++)
        {
            GameManager.instance.titles[i].ResetTile();
        }
    }
}
