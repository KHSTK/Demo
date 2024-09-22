using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeReference] private Sprite[] sprite;

    public bool canWalk;
    public bool canMove;
    public LayerMask obLayerMask;
    public Color HighlightColor;

    private void Start()
    {
        //GameManage.instance.tile = null;
        Array.Clear(GameManage.instance.tiles, 0, GameManage.instance.tiles.Length);
        //GameManage.instance.tiles = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        CheckObstacle();
        Application.targetFrameRate = 60;
        GameManage.instance.tile.Add(this);
        GameManage.instance.tiles = GameManage.instance.tile.ToArray();
    }
    private void OnMouseDown()
    {
        if (canMove&&canWalk && GameManage.instance.selectedUnit != null/*&&!GameManage.instance.selectedUnit.hasMove*/)
        {
            GameManage.instance.selectedUnit.Move(this.transform);
            GameManage.instance.selectedUnit.hasMove = true;
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.005f;
        spriteRenderer.sortingOrder = 25;
    }
    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.005f;
        spriteRenderer.sortingOrder = 0;
    }
    private void CheckObstacle()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, spriteRenderer.bounds.extents.x, obLayerMask);
        if (collider != null)
        {
            Debug.Log("false");
            canWalk = false;
        }
        else
        {
            Debug.Log("true");
            canWalk = true;
        }
    }
    public void HighlightTile()
    {
        if (canWalk)
        {
            spriteRenderer.color = HighlightColor;
            canMove = true;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    public void ResetTile()
    {
        spriteRenderer.color = Color.white;
        canMove = false;
    }

}
